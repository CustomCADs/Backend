using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetTracks;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Get.Track;

public class TrackShipmentMapper : ResponseMapper<TrackShipmentResponse, GetShipmentTracksDto>
{
	public override TrackShipmentResponse FromEntity(GetShipmentTracksDto track)
		=> new(
			Message: track.Message,
			Place: track.Place
		);

}
