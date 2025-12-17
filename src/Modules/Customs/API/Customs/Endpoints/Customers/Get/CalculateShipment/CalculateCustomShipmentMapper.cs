using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.CalculateShipment;

public class CalculateCustomShipmentMapper : ResponseMapper<CalculateCustomShipmentResponse, CalculateShipmentDto>
{
	public override CalculateCustomShipmentResponse FromEntity(CalculateShipmentDto calculation)
		=> new(
			Service: calculation.Service,
			Total: calculation.Total,
			Currency: calculation.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline
		);
};
