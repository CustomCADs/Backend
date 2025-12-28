using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetTracks;

public sealed class GetShipmentTracksHandler(
	IShipmentReads reads,
	IDeliveryService delivery
) : IQueryHandler<GetShipmentTracksQuery, Dictionary<DateTimeOffset, GetShipmentTracksDto>>
{
	public async Task<Dictionary<DateTimeOffset, GetShipmentTracksDto>> Handle(GetShipmentTracksQuery req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		if (shipment is not { Status: ShipmentStatus.Active, Reference.Id: not null })
		{
			throw CustomStatusException<Shipment>.ById(req.Id);
		}

		ShipmentTrackDto[] statuses = await delivery.TrackAsync(
			shipmentId: shipment.Reference.Id,
			ct: ct
		).ConfigureAwait(false);

		return statuses.ToDictionary(
			x => x.DateTime,
			x => new GetShipmentTracksDto(x.Message, x.Place)
		);
	}
}
