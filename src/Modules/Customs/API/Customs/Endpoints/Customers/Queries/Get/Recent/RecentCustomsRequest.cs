namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.Recent;

public sealed record RecentCustomsRequest(
	int Limit = 5
);
