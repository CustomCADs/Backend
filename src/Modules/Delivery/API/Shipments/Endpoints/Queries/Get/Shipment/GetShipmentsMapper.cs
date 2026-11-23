using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Shipment;

public class GetShipmentMapper : ResponseMapper<GetShipmentResponse, GetShipmentDto>
{
	public override GetShipmentResponse FromEntity(GetShipmentDto shipment)
		=> new(
			Id: shipment.Id.Value,
			RequestedAt: shipment.RequestedAt,
			Status: shipment.Status,
			Info: shipment.Info.ToResponse(),
			Fee: shipment.Fee
		);
}
