using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetTracks;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Track;

public class TrackShipmentEndpoint(IRequestSender sender)
	: Endpoint<TrackShipmentRequest, Dictionary<DateTimeOffset, TrackShipmentResponse>>
{
	public override void Configure()
	{
		Get("{id}/track");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Track")
			.WithDescription("See the tracking history of your shipment")
		);
	}

	public override async Task HandleAsync(TrackShipmentRequest req, CancellationToken ct)
	{
		Dictionary<DateTimeOffset, GetShipmentTracksDto> tracks = await sender.SendQueryAsync(
			query: new GetShipmentTracksQuery(
				Id: ShipmentId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		Dictionary<DateTimeOffset, TrackShipmentResponse> response = tracks.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
