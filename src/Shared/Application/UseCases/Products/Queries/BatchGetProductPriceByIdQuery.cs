namespace CustomCADs.Shared.Application.UseCases.Products.Queries;

public sealed record BatchGetProductPriceByIdQuery(
	ProductId[] Ids
) : IQuery<Dictionary<ProductId, decimal>>;
