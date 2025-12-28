namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Post.Item;

public sealed record PostActiveCartItemRequest(
	Guid ProductId,
	Guid? CustomizationId,
	bool ForDelivery
);
