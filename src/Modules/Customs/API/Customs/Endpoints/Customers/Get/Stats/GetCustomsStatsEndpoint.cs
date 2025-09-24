using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Stats;

public sealed class GetCustomsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<GetCustomsStatsResponse, GetCustomsStatsMapper>
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

		await Send.MappedAsync(counts, Map.FromEntity).ConfigureAwait(false);
	}
}
