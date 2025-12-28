using CustomCADs.Modules.Customs.Application.Customs.Dtos;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed record CustomerGetCustomByIdDto(
	CustomId Id,
	string Name,
	string Description,
	bool ForDelivery,
	CustomStatus CustomStatus,
	DateTimeOffset OrderedAt,
	CustomCategoryDto? Category,
	AcceptedCustomDto? AcceptedCustom,
	FinishedCustomDto? FinishedCustom,
	CompletedCustomDto? CompletedCustom
);
