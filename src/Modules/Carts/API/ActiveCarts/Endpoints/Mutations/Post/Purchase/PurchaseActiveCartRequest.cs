namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.Purchase;

public sealed record PurchaseActiveCartRequest(
	string PaymentMethodId
);
