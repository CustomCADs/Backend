using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Stats;

public sealed class GetCustomsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<GetCustomsStatsResponse>
{
	public override void Configure()
	{
		Get("stats");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Stats")
			.WithDescription("See your Custom' stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		CountCustomsDto counts = await sender.SendQueryAsync(
			query: new CountCustomsQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		GetCustomsStatsResponse response = new(
			PendingCount: counts.Pending,
			AcceptedCount: counts.Accepted,
			BegunCount: counts.Begun,
			FinishedCount: counts.Finished,
			CompletedCount: counts.Completed,
			ReportedCount: counts.Reported
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
