using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

public sealed class GetCustomPaymentStatusesHandler : IQueryHandler<GetCustomPaymentStatusesQuery, PaymentStatus[]>
{
	public Task<PaymentStatus[]> Handle(GetCustomPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<PaymentStatus>()
		);
}
