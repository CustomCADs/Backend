using CustomCADs.Modules.Customs.Application.Customs.Dtos;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public sealed record DesignerGetCustomByIdDto(
	CustomId Id,
	string Name,
	string Description,
	bool ForDelivery,
	string BuyerName,
	CustomStatus CustomStatus,
	DateTimeOffset OrderedAt,
	CustomCategoryDto? Category,
	AcceptedCustomDto? AcceptedCustom,
	FinishedCustomDto? FinishedCustom,
	CompletedCustomDto? CompletedCustom
);
