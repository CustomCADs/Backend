namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Patch.DecrementQuantity;

public record DecreaseActiveCartItemQuantityRequest(
	Guid ProductId,
	int Amount = 1
);
