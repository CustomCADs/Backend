namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Recent;

public sealed record RecentCustomsResponse(
	Guid Id,
	string Name,
	DateTimeOffset OrderedAt,
	string? DesignerName
);
