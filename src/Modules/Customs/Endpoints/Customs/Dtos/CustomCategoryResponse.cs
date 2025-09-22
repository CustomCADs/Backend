using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record CustomCategoryResponse(
	int Id,
	string Name,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
);
