using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetTracks;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Track;

namespace CustomCADs.Delivery.Endpoints.Shipments;

internal static class Mapper
{
	internal static Dictionary<DateTimeOffset, TrackShipmentResponse> ToResponse(this Dictionary<DateTimeOffset, GetShipmentTracksDto> tracks)
		=> tracks.ToDictionary(
			x => x.Key,
			x => new TrackShipmentResponse(x.Value.Message, x.Value.Place)
		);

	internal static GetShipmentsResponse ToResponse(this GetAllShipmentsDto shipment)
		=> new(
			Id: shipment.Id.Value,
			RequestedAt: shipment.RequestedAt,
			Status: shipment.Status,
			Info: shipment.Info.ToResponse(),
			Address: shipment.Address.ToResponse()
		);

	private static InfoResponse ToResponse(this ShipmentInfo info)
		=> new(
			Count: info.Count,
			Weight: info.Weight,
			Recipient: info.Recipient
		);

	private static AddressResponse ToResponse(this ShipmentAddress address)
		=> new(
			Country: address.Country,
			City: address.City,
			Street: address.Street
		);
}
