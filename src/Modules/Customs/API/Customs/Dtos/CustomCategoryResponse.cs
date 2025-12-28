using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Dtos;

public record CustomCategoryResponse(
	int Id,
	string Name,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
);
