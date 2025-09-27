using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Get.CalculateShipment;

public class CalculateActiveCartShipmentMappper : ResponseMapper<CalculateActiveCartShipmentResponse, CalculateShipmentDto>
{
	public override CalculateActiveCartShipmentResponse FromEntity(CalculateShipmentDto calculation)
		=> new(
			Service: calculation.Service,
			Total: calculation.Total,
			Currency: calculation.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline
		);
}
