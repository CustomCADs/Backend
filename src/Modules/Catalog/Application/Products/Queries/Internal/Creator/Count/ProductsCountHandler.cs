using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;

public sealed class ProductsCountHandler(IProductReads reads)
	: IQueryHandler<ProductsCountQuery, ProductsCountDto>
{
	public async Task<ProductsCountDto> Handle(ProductsCountQuery req, CancellationToken ct)
	{
		Dictionary<ProductStatus, int> counts = await reads
			.CountByStatusAsync(req.CallerId, ct: ct).ConfigureAwait(false);

		return new(
			Unchecked: counts.GetCountOrZero(ProductStatus.Unchecked),
			Validated: counts.GetCountOrZero(ProductStatus.Validated),
			Reported: counts.GetCountOrZero(ProductStatus.Reported),
			Banned: counts.GetCountOrZero(ProductStatus.Removed)
		);
	}
}
