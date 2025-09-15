namespace CustomCADs.Shared.Application.UseCases.Products.Queries;

public sealed record GetProductPricesByIdsQuery(
	ProductId[] Ids
) : IQuery<Dictionary<ProductId, decimal>>;
