using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

public sealed class GetPurchasedCartPaymentStatusesHandler : IQueryHandler<GetPurchasedCartPaymentStatusesQuery, PaymentStatus[]>
{
	public Task<PaymentStatus[]> Handle(GetPurchasedCartPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<PaymentStatus>()
		);
}
