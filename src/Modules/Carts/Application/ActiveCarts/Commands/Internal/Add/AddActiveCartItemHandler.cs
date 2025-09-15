using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;

public sealed class AddActiveCartItemHandler(
	IWrites<ActiveCartItem> writes,
	IUnitOfWork uow,
	IRequestSender sender
) : ICommandHandler<AddActiveCartItemCommand>
{
	public async Task Handle(AddActiveCartItemCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetProductExistsByIdQuery(req.ProductId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<ActiveCartItem>.ById(req.CallerId, "User");
		}

		await writes.AddAsync(
			entity: await CreateItemAsync(req, ct).ConfigureAwait(false),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}

	private async Task<ActiveCartItem> CreateItemAsync(AddActiveCartItemCommand req, CancellationToken ct)
	{
		if (!req.ForDelivery)
		{
			return ActiveCartItem.Create(req.ProductId, req.CallerId);
		}

		if (req.CustomizationId is null)
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);
		}
		CustomizationId customizationId = req.CustomizationId.Value;

		if (!await sender.SendQueryAsync(new GetCustomizationExistsByIdQuery(customizationId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<ActiveCartItem>.ById(customizationId, "Customization");
		}
		return ActiveCartItem.Create(req.ProductId, req.CallerId, customizationId);
	}
}
