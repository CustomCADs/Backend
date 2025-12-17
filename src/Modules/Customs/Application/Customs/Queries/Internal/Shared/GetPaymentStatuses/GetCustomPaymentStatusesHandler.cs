using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

public sealed class GetCustomPaymentStatusesHandler : IQueryHandler<GetCustomPaymentStatusesQuery, PaymentStatus[]>
{
	public Task<PaymentStatus[]> Handle(GetCustomPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<PaymentStatus>()
		);
}
