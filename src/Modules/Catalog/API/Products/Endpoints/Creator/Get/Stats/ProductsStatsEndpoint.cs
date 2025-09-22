using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Stats;

public sealed class ProductsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductsStatsResponse>
{
	public override void Configure()
	{
		Get("stats");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Stats")
			.WithDescription("See your Products' stats")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ProductsCountDto counts = await sender.SendQueryAsync(
			query: new ProductsCountQuery(
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		ProductsStatsResponse response = new(
			UncheckedCount: counts.Unchecked,
			ValidatedCount: counts.Validated,
			ReportedCount: counts.Reported,
			BannedCount: counts.Banned
		);
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
