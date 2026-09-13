using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Create.ToOffice;

public sealed class CreateShipmentToOfficeHandler(
	IWrites<Shipment> writes,
	IUnitOfWork uow,
	IRequestSender sender,
	IDeliveryService delivery
) : ICommandHandler<CreateShipmentToOfficeCommand, ShipmentId>
{
	public async Task<ShipmentId> Handle(CreateShipmentToOfficeCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Shipment>.ById(req.BuyerId, "User");
		}

		bool isDeliveryDetailsValid = await delivery.ValidateAsync(
			phone: req.Contact.Phone,
			ct: ct
		).ConfigureAwait(false);
		if (!isDeliveryDetailsValid)
		{
			throw new CustomException("Country, City, Street and Phone are not as Delivery provider expects them!");
		}

		Shipment shipment = await writes.AddAsync(
			entity: Shipment.Create(
				buyerId: req.BuyerId,
				delivery: new(DeliveryDestination.Office, OfficeId: req.OfficeId),
				service: req.Service,
				email: req.Contact.Email,
				phone: req.Contact.Phone,
				recipient: req.Info.Recipient,
				count: req.Info.Count,
				weight: req.Info.Weight
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return shipment.Id;
	}
}
