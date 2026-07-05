namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public record Theory(
	string PaymentMethodId,
	string ShipmentService,
	string Country,
	string City,
	string Street,
	string? Phone,
	string? Email
);
