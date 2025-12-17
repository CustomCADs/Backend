using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetAll;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Get.Shipment;

public class GetShipmentsMapper : ResponseMapper<GetShipmentsResponse, GetAllShipmentsDto>
{
	public override GetShipmentsResponse FromEntity(GetAllShipmentsDto shipment)
		=> new(
			Id: shipment.Id.Value,
			RequestedAt: shipment.RequestedAt,
			Status: shipment.Status,
			Info: shipment.Info.ToResponse(),
			Address: shipment.Address.ToResponse()
		);
}
