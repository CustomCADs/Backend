using CustomCADs.Modules.Customs.API.Customs.Dtos;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Get.Single;

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
