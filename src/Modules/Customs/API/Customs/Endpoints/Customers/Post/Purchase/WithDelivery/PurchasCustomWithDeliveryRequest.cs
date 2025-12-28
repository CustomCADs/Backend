using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Post.Purchase.WithDelivery;

public sealed record PurchasCustomWithDeliveryRequest(
	Guid Id,
	AddressDto Address,
	ContactDto Contact,
	string PaymentMethodId,
	string ShipmentService,
	int Count,
	Guid CustomizationId
);
