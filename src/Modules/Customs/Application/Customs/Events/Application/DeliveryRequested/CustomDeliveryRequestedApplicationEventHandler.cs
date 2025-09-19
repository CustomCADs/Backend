using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;

public class CustomDeliveryRequestedApplicationEventHandler(ICustomReads reads, IRequestSender sender)
{
	public async Task Handle(CustomDeliveryRequestedApplicationEvent ae)
	{
		Custom custom = await reads.SingleByIdAsync(ae.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(ae.Id);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(custom.BuyerId)
		).ConfigureAwait(false);
		int count = ae.Count;
		double weight = ae.Weight;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			command: new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: ae.ShipmentService,
				Address: ae.Address,
				Contact: ae.Contact,
				BuyerId: custom.BuyerId
			)
		).ConfigureAwait(false);
		custom.SetShipment(shipmentId);
	}
}
