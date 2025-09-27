namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Recent;

public sealed record RecentCustomsRequest(
	int Limit = 5
);
