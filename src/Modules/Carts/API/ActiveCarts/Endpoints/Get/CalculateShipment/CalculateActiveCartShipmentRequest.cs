namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Get.CalculateShipment;

public record CalculateActiveCartShipmentRequest(
	string Country,
	string City,
	string Street
);
