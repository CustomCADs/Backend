using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.All;

public sealed record GetCustomsResponse(
	Guid Id,
	string Name,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery,
	string? DesignerName,
	string? CategoryName
);
