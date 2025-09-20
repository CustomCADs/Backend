using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Shared.Activate;

public class ActivateShipmentHandler(
	IShipmentReads reads,
	IUnitOfWork uow,
	IDeliveryService delivery
) : ICommandHandler<ActivateShipmentCommand>
{
	public async Task Handle(ActivateShipmentCommand req, CancellationToken ct = default)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		ShipmentDto reference = await delivery.ShipAsync(
			req: new(
				Package: "BOX",
				Contents: $"{shipment.Info.Count} 3D Model/s, wrapped together in a box",
				Name: shipment.Info.Recipient,
				TotalWeight: shipment.Info.Weight,
				Service: shipment.Reference.Service,
				Country: shipment.Address.Country,
				City: shipment.Address.City,
				Street: shipment.Address.Street,
				Phone: shipment.Contact.Phone,
				Email: shipment.Contact.Email
			),
			ct: ct
		).ConfigureAwait(false);

		shipment.Activate(reference.Id);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
