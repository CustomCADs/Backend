using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.Application.Customs.Dtos;

public record CustomCategoryDto(
	CategoryId Id,
	string Name,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
);
