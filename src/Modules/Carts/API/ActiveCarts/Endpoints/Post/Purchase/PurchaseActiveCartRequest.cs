namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Post.Purchase;

public sealed record PurchaseActiveCartRequest(
	string PaymentMethodId
);
