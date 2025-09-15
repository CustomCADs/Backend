namespace CustomCADs.Shared.Application.UseCases.Products.Queries;

public sealed record GetProductExistsByIdQuery(ProductId Id) : IQuery<bool>;
