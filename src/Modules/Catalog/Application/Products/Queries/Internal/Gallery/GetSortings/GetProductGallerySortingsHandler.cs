using CustomCADs.Catalog.Application.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

public sealed class GetProductGallerySortingsHandler : IQueryHandler<GetProductGallerySortingsQuery, ProductGallerySortingType[]>
{
	public Task<ProductGallerySortingType[]> Handle(GetProductGallerySortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<ProductGallerySortingType>()
		);
}
