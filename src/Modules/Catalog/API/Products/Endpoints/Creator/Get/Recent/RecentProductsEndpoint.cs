using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Recent;

public sealed class RecentProductsEndpoint(IRequestSender sender)
	: Endpoint<RecentProductsRequest, RecentProductsResponse[]>
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

		RecentProductsResponse[] response = [.. result.Items.Select(x => x.ToRecentResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
