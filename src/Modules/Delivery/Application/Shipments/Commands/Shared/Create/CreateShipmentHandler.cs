using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Create;

public sealed class CreateShipmentHandler(
	IWrites<Shipment> writes,
	IUnitOfWork uow,
	IRequestSender sender,
	IDeliveryService delivery
) : ICommandHandler<CreateShipmentCommand, ShipmentId>
{
	public async Task<ShipmentId> Handle(CreateShipmentCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Shipment>.ById(req.BuyerId, "User");
		}

		bool isDeliveryDetailsValid = await delivery.ValidateAsync(
			country: req.Address.Country,
			city: req.Address.City,
			street: req.Address.Street,
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
				service: req.Service,
				email: req.Contact.Email,
				phone: req.Contact.Phone,
				recipient: req.Info.Recipient,
				count: req.Info.Count,
				weight: req.Info.Weight,
				country: req.Address.Country,
				city: req.Address.City,
				street: req.Address.Street
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return shipment.Id;
	}
}
