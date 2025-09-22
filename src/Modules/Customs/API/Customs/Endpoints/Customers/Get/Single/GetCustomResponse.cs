using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.API.Customs.Dtos;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Single;

public sealed record GetCustomResponse(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery,
	CustomCategoryResponse? Category,
	AcceptedCustomResponse? AcceptedCustom,
	FinishedCustomResponse? FinishedCustom,
	CompletedCustomResponse? CompletedCustom
);
