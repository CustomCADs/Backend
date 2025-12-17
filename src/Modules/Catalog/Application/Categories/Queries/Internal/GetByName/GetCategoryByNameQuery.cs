namespace CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetByName;

public sealed record GetCategoryByNameQuery(
	string Name
) : IQuery<CategoryReadDto>;
