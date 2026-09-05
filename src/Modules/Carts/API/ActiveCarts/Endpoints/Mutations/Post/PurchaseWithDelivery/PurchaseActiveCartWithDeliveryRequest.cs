using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery;

public sealed record PurchaseActiveCartWithDeliveryRequest(
	string PaymentMethodId,
	string ShipmentService,
	AddressDto Address,
	ContactDto Contact
);
