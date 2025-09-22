namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Patch.IncrementQuantity;

public record IncreaseActiveCartItemQuantityRequest(
	Guid ProductId,
	int Amount = 1
);
