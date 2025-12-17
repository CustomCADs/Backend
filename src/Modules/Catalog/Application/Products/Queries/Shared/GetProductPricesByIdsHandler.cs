using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;

public sealed class GetProductPricesByIdsHandler(IProductReads reads)
	: IQueryHandler<GetProductPricesByIdsQuery, Dictionary<ProductId, decimal>>
{
	public async Task<Dictionary<ProductId, decimal>> Handle(GetProductPricesByIdsQuery req, CancellationToken ct)
	{
		Result<Product> result = await reads.AllAsync(
			query: new(
				Pagination: new(1, req.Ids.Length),
				Ids: req.Ids
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		return result.Items.ToDictionary(x => x.Id, x => x.Price);
	}
}
