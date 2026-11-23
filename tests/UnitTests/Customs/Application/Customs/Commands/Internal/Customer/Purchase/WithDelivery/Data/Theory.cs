namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;

public record Theory(
	string PaymentMethodId,
	int Count,
	string ShipmentService,
	string Phone,
	string Email
);
