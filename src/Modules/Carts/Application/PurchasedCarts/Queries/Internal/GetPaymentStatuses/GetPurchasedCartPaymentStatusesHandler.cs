using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

public sealed class GetPurchasedCartPaymentStatusesHandler : IQueryHandler<GetPurchasedCartPaymentStatusesQuery, PaymentStatus[]>
{
	public Task<PaymentStatus[]> Handle(GetPurchasedCartPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<PaymentStatus>()
		);
}
