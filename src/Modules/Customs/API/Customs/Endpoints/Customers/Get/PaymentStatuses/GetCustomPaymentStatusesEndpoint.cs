using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.PaymentStatuses;

public sealed class GetCustomPaymentStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<PaymentStatus[]>
{
	public override void Configure()
	{
		Get("payment-statuses");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Payment Statuses")
			.WithDescription("See all Custom Payment Status types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		PaymentStatus[] result = await sender.SendQueryAsync(
			query: new GetCustomPaymentStatusesQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
