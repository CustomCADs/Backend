using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.Domain.Customs.ValueObjects;

public record CustomCategory(
	CategoryId Id,
	CustomId CustomId,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
);
