namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Put;

public sealed record PutCategoryRequest(
	int Id,
	string Name,
	string Description
);
