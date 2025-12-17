using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.Count;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Stats;

public sealed class ProductsStatsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductsStatsResponse, ProductsStatsMapper>
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
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(counts, Map.FromEntity).ConfigureAwait(false);
	}
}
