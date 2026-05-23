using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;

public class ActiveCartDeliveryRequestedHandler(
	IPurchasedCartReads reads,
	IUnitOfWork uow,
	IRequestSender sender
)
{
	public async Task HandleAsync(ActiveCartDeliveryRequestedApplicationEvent @event)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(@event.PurchasedCartId).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(@event.PurchasedCartId);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(cart.BuyerId)
		).ConfigureAwait(false);
		double weight = @event.Weight;
		int count = @event.Count;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			command: new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: @event.ShipmentService,
				Address: @event.Address,
				Contact: @event.Contact,
				BuyerId: cart.BuyerId
			)
		).ConfigureAwait(false);

		cart.SetShipmentId(shipmentId);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
