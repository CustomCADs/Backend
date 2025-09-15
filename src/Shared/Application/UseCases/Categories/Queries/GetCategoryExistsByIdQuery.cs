namespace CustomCADs.Shared.Application.UseCases.Categories.Queries;

public sealed record GetCategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
