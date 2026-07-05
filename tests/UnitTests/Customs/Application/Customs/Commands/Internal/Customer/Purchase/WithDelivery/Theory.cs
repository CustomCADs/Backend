namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

public record Theory(
	string PaymentMethodId,
	int Count,
	string ShipmentService,
	string Country,
	string City,
	string Street,
	string? Phone,
	string? Email
);
