namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Patch.IncrementQuantity;

public record IncreaseActiveCartItemQuantityRequest(
	Guid ProductId,
	int Amount = 1
);
