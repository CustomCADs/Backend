namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.Stats;

public sealed record GetCustomsStatsResponse(
	int PendingCount,
	int AcceptedCount,
	int BegunCount,
	int FinishedCount,
	int CompletedCount,
	int ReportedCount
);
