namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.Item;

public sealed record PostActiveCartItemRequest(
	Guid ProductId,
	Guid? CustomizationId,
	bool ForDelivery
);
