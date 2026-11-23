using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Modules.Delivery.API.Shipments;

internal static class Mapper
{
	extension(ShipmentInfo info)
	{
		internal InfoResponse ToResponse()
			=> new(
				Count: info.Count,
				Weight: info.Weight,
				Recipient: info.Recipient
			);
	}
}
