namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Patch.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryRequest(
	Guid ProductId,
	Guid? CustomizationId
);
