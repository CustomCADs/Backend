namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlRequest(
	Guid Id,
	Guid ProductId
);
