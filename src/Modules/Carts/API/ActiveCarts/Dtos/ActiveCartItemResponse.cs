namespace CustomCADs.Modules.Carts.API.ActiveCarts.Dtos;

public sealed record ActiveCartItemResponse(
	int Quantity,
	bool ForDelivery,
	DateTimeOffset AddedAt,
	Guid ProductId,
	Guid? CustomizationId
);
