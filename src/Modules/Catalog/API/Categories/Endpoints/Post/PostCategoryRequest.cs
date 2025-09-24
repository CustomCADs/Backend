namespace CustomCADs.Catalog.API.Categories.Endpoints.Post;

public sealed record PostCategoryRequest(
	string Name,
	string Description
);
