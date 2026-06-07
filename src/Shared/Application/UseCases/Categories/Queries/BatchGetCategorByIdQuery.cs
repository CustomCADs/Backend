namespace CustomCADs.Shared.Application.UseCases.Categories.Queries;

public sealed record BatchGetCategorByIdQuery(
	CategoryId[] Ids
) : IQuery<Dictionary<CategoryId, string>>;
