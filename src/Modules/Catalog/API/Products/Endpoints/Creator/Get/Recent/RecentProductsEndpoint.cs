using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Recent;

public sealed class RecentProductsEndpoint(IRequestSender sender)
	: Endpoint<RecentProductsRequest, RecentProductsResponse[], RecentProductsMapper>
{
	public override void Configure()
	{
		Get("recent");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Recent")
			.WithDescription("See your most recent Products")
		);
	}

	public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
	{
		Result<CreatorGetAllProductsDto> result = await sender.SendQueryAsync(
			query: new CreatorGetAllProductsQuery(
				CallerId: User.GetAccountId(),
				Sorting: new(
					ProductCreatorSortingType.UploadedAt.ToBase(),
					SortingDirection.Descending
				),
				Pagination: new(Limit: req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync([.. result.Items], Map.FromEntity).ConfigureAwait(false);
	}
}
