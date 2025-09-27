using CustomCADs.Customs.API.Customs.Dtos;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Endpoints.Designer.Get.Single;

public sealed record GetCustomResponse(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery,
	string BuyerName,
	CustomCategoryResponse? Category,
	AcceptedCustomResponse? AcceptedCustom,
	FinishedCustomResponse? FinishedCustom,
	CompletedCustomResponse? CompletedCustom
);
