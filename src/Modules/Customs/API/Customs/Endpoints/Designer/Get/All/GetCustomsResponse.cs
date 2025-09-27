using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Endpoints.Designer.Get.All;

public sealed record GetCustomsResponse(
	Guid Id,
	string Name,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery,
	string BuyerName,
	string? CategoryName
);
