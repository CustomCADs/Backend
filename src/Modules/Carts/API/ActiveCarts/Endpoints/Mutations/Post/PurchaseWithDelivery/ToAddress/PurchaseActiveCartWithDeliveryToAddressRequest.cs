using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery.ToAddress;

public sealed record PurchaseActiveCartWithDeliveryToAddressRequest(
	string PaymentMethodId,
	string ShipmentService,
	AddressDto Address,
	ContactDto Contact
);
