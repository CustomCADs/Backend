using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public sealed record GetAllCustomsDto(
	CustomId Id,
	string Name,
	bool ForDelivery,
	CustomStatus CustomStatus,
	DateTimeOffset OrderedAt,
	string BuyerName,
	string? DesignerName,
	string? CategoryName
);
