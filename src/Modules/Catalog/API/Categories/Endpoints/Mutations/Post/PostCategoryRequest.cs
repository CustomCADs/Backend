namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Mutations.Post;

public sealed record PostCategoryRequest(
	string Name,
	string Description
);
