namespace CustomCADs.Catalog.API.Categories.Dtos;

public sealed record CategoryResponse(
	int Id,
	string Name,
	string Description
);
