using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Dtos;

public record CustomCategoryResponse(
	int Id,
	string Name,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
);
