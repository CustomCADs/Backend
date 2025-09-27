using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Carts.API.ActiveCarts;

internal static class Mapper
{
	internal static ActiveCartItemResponse ToResponse(this ActiveCartItemDto item)
		=> new(
			Quantity: item.Quantity,
			ForDelivery: item.ForDelivery,
			AddedAt: item.AddedAt,
			ProductId: item.ProductId.Value,
			CustomizationId: item.CustomizationId?.Value
		);

	internal static PaymentResponse ToResponse(this PaymentDto payment)
		=> new(
			ClientSecret: payment.ClientSecret,
			Message: payment.Message
		);
}
