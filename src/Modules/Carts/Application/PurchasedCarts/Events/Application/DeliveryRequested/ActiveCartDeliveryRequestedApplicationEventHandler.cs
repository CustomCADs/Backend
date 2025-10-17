using CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;

public class ActiveCartDeliveryRequestedApplicationEventHandler(
	IPurchasedCartReads reads,
	IUnitOfWork uow,
	IRequestSender sender
)
{
	public async Task Handle(ActiveCartDeliveryRequestedApplicationEvent ae)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(ae.PurchasedCartId).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(ae.PurchasedCartId);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(cart.BuyerId)
		).ConfigureAwait(false);
		double weight = ae.Weight;
		int count = ae.Count;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			command: new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: ae.ShipmentService,
				Address: ae.Address,
				Contact: ae.Contact,
				BuyerId: cart.BuyerId
			)
		).ConfigureAwait(false);

		cart.SetShipmentId(shipmentId);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
