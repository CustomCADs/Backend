using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Commands.Internal.Cancel;

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
		shipment.Cancel();

		if (shipment is not { Reference.Id: not null })
		{
			throw CustomStatusException<Shipment>.ById(req.Id);
		}

		await delivery.CancelAsync(
			shipmentId: shipment.Reference.Id,
			comment: req.Comment,
			ct: ct
		).ConfigureAwait(false);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
