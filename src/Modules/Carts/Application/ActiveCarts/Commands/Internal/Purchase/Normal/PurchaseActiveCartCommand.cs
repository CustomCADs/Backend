using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

public sealed record PurchaseActiveCartCommand(
	string PaymentMethodId,
	AccountId CallerId
) : ICommand<PaymentDto>;
