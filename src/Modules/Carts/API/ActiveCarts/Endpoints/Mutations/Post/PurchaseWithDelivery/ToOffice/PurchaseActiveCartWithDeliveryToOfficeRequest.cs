using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Mutations.Post.PurchaseWithDelivery.ToOffice;

public sealed record PurchaseActiveCartWithDeliveryToOfficeRequest(
	string PaymentMethodId,
	string ShipmentService,
	int OfficeId,
	ContactDto Contact
);
