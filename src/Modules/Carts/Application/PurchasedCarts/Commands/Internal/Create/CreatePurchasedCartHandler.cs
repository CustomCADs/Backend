using CustomCADs.Modules.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public sealed class CreatePurchasedCartHandler(
	IWrites<PurchasedCart> writes,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
	public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<PurchasedCart>.ById(req.BuyerId, "User");
		}
		CartItemDto[] items = await SnapshotItemsAsync(req.Items, req.BuyerId, ct).ConfigureAwait(false);

		PurchasedCart cart = await writes.AddAsync(
			entity: PurchasedCart.Create(req.BuyerId).AddItems(items),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserPurchasedProductApplicationEvent(
				Ids: [.. items.Select(x => x.ProductId)]
			)
		).ConfigureAwait(false);

		return cart.Id;
	}

	private async Task<CartItemDto[]> SnapshotItemsAsync(Dictionary<ActiveCartItemDto, decimal> items, AccountId buyerId, CancellationToken ct = default)
	{
		ProductId[] productIds = [.. items.Select(x => x.Key.ProductId)];

		Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(
			query: new GetProductCadIdsByIdsQuery(productIds),
			ct: ct
		).ConfigureAwait(false);

		Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(
			command: new DuplicateCadsByIdsCommand(
				Ids: [.. productCads.Select(x => x.Value)],
				CallerId: buyerId
			),
			ct: ct
		).ConfigureAwait(false);

		return [.. items.Select((x) =>
		{
			(ActiveCartItemDto Item, decimal Price) = (x.Key, x.Value);
			CadId productCadId = productCads[Item.ProductId];
			CadId itemCadId = itemCads[productCadId];

			return new CartItemDto(
				Price: Price,
				CadId: itemCadId,
				ProductId: Item.ProductId,
				ForDelivery: Item.ForDelivery,
				CustomizationId: Item.CustomizationId,
				Quantity: Item.Quantity,
				AddedAt: Item.AddedAt
			);
		})];
	}
}
