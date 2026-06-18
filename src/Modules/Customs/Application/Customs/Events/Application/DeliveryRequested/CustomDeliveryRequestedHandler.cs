using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Modules.Customs.Application.Customs.Events.Application.DeliveryRequested;

public class CustomDeliveryRequestedHandler(
	ICustomReads reads,
	IUnitOfWork uow,
	IRequestSender sender
) : IEventHandler<CustomDeliveryRequestedApplicationEvent>
{
	public async Task HandleAsync(CustomDeliveryRequestedApplicationEvent @event)
	{
		Custom custom = await reads.SingleByIdAsync(@event.CustomId).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(@event.CustomId);

		string buyer = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(custom.BuyerId)
		).ConfigureAwait(false);
		int count = @event.Count;
		double weight = @event.Weight;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			command: new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: @event.ShipmentService,
				Address: @event.Address,
				Contact: @event.Contact,
				BuyerId: custom.BuyerId
			)
		).ConfigureAwait(false);

		custom.SetShipment(shipmentId);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
