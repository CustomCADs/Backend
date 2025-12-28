using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Customizations.Commands;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

public sealed class ToggleActiveCartItemForDeliveryHandler(
	IActiveCartReads reads,
	IUnitOfWork uow,
	IRequestSender sender
) : ICommandHandler<ToggleActiveCartItemForDeliveryCommand>
{
	public async Task Handle(ToggleActiveCartItemForDeliveryCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.CallerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.CallerId, req.ProductId });

		if (item.ForDelivery)
		{
			await TurnDeliveryOffAsync(item, item.CustomizationId, ct).ConfigureAwait(false);
			return;
		}

		if (req.CustomizationId is null)
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);
		}
		await TurnDeliveryOnAsync(item, req.CustomizationId.Value, ct).ConfigureAwait(false);
	}

	private async Task TurnDeliveryOffAsync(ActiveCartItem item, CustomizationId? customizationId, CancellationToken ct = default)
	{
		item.SetNoDelivery();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (customizationId is not null)
		{
			await sender.SendCommandAsync(
				new DeleteCustomizationByIdCommand(customizationId.Value),
				ct: ct
			).ConfigureAwait(false);
		}
	}

	private async Task TurnDeliveryOnAsync(ActiveCartItem item, CustomizationId customizationId, CancellationToken ct = default)
	{
		if (!await sender.SendQueryAsync(new GetCustomizationExistsByIdQuery(customizationId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<ActiveCartItem>.ById(customizationId, "Customization");
		}

		item.SetForDelivery(customizationId);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
