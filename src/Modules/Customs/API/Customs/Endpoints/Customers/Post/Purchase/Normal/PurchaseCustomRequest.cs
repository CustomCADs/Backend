namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Post.Purchase.Normal;

public sealed record PurchaseCustomRequest(
	Guid Id,
	string PaymentMethodId
);
