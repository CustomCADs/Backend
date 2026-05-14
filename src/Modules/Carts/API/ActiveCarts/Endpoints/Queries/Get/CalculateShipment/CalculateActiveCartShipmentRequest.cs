namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Queries.Get.CalculateShipment;

public record CalculateActiveCartShipmentRequest(
	string Country,
	string City,
	string Street
);
