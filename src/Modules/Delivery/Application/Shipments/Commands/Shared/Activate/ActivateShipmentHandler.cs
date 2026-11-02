using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Commands.Shared.Activate;

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
				Destination: shipment.Delivery.Destination,
				OfficeId: shipment.Delivery.OfficeId,
				Address: shipment.Delivery.Address,
				Package: "BOX",
				Contents: $"{shipment.Info.Count} 3D Model/s, wrapped together in a box",
				Name: shipment.Info.Recipient,
				TotalWeight: shipment.Info.Weight,
				Service: shipment.Reference.Service,
				Phone: shipment.Contact.Phone,
				Email: shipment.Contact.Email
			),
			ct: ct
		).ConfigureAwait(false);

		shipment.Activate(reference.Id);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
