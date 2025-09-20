using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;

public sealed class CancelShipmentHandler(
	IShipmentReads reads,
	IUnitOfWork uow,
	IDeliveryService delivery
) : ICommandHandler<CancelShipmentCommand>
{
	public async Task Handle(CancelShipmentCommand req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		if (shipment is not { Status: ShipmentStatus.Active, Reference.Id: not null })
		{
			throw CustomStatusException<Shipment>.ById(req.Id);
		}

		shipment.Cancel();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await delivery.CancelAsync(
			shipmentId: shipment.Reference.Id,
			comment: req.Comment,
			ct: ct
		).ConfigureAwait(false);
	}
}
