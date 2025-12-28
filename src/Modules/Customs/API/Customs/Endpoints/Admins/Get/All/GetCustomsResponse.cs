using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Admins.Get.All;

public sealed record GetCustomsResponse(
	Guid Id,
	string Name,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery,
	string BuyerName,
	string? DesignerName,
	string? CategoryName
);
