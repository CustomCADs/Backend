namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Delete;

public sealed record DeleteActiveCartItemRequest(
	Guid ProductId
);
