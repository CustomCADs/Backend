namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Patch.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryRequest(
	Guid ProductId,
	Guid? CustomizationId
);
