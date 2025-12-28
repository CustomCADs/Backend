using CustomCADs.Modules.Catalog.Application.Products.Enums;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

public sealed class GetProductDesignerSortingsHandler : IQueryHandler<GetProductDesignerSortingsQuery, ProductDesignerSortingType[]>
{
	public Task<ProductDesignerSortingType[]> Handle(GetProductDesignerSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
				Enum.GetValues<ProductDesignerSortingType>()
			);
}
