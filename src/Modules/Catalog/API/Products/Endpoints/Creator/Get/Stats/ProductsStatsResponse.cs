namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Stats;

public sealed record ProductsStatsResponse(
	int UncheckedCount,
	int ValidatedCount,
	int ReportedCount,
	int BannedCount
);
