using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToOffice;

public sealed record PurchaseActiveCartWithDeliveryToOfficeCommand(
	string PaymentMethodId,
	string ShipmentService,
	int OfficeId,
	ContactDto Contact,
	AccountId CallerId
) : ICommand<PaymentDto>;
