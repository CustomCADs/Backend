namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Mutations.Post.Purchase.Normal;

public sealed record PurchaseCustomRequest(
	Guid Id,
	string PaymentMethodId
);
