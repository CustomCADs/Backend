using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Recent;

public sealed class RecentCustomsEndpoint(IRequestSender sender)
	: Endpoint<RecentCustomsRequest, RecentCustomsResponse[], RecentCustomsMapper>
{
	public override void Configure()
	{
		Get("recent");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Recent")
			.WithDescription("See your most recent Customs")
		);
	}

	public override async Task HandleAsync(RecentCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			query: new GetAllCustomsQuery(
				CustomerId: User.GetAccountId(),
				Sorting: new(CustomSortingType.OrderedAt, SortingDirection.Descending),
				Pagination: new(Limit: req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result.Items, Map.FromEntity).ConfigureAwait(false);
	}
}
