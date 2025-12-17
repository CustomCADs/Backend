using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.Count;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Stats;

public class ProductsStatsMapper : ResponseMapper<ProductsStatsResponse, ProductsCountDto>
{
	public override ProductsStatsResponse FromEntity(ProductsCountDto counts)
		=> new(
			UncheckedCount: counts.Unchecked,
			ValidatedCount: counts.Validated,
			ReportedCount: counts.Reported,
			BannedCount: counts.Banned
		);
}
