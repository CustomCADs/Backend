using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Modules.Carts.API.ActiveCarts;

internal static class Mapper
{
	extension(ActiveCartItemDto item)
	{
		internal ActiveCartItemResponse ToResponse()
			=> new(
				Quantity: item.Quantity,
				ForDelivery: item.ForDelivery,
				AddedAt: item.AddedAt,
				ProductId: item.ProductId.Value,
				CustomizationId: item.CustomizationId?.Value
			);
	}

	extension(PaymentDto payment)
	{
		internal PaymentResponse ToResponse()
			=> new(
				ClientSecret: payment.ClientSecret,
				Message: payment.Message
			);
	}

}
