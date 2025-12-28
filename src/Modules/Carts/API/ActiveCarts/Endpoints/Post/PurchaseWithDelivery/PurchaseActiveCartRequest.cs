using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Post.PurchaseWithDelivery;

public sealed record PurchaseActiveCartRequest(
	string PaymentMethodId,
	string ShipmentService,
	AddressDto Address,
	ContactDto Contact
);
