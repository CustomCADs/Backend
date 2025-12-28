namespace CustomCADs.Modules.Carts.API.ActiveCarts.Dtos;

public record PaymentResponse(
	string ClientSecret,
	string Message
);
