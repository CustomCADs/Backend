using CustomCADs.Catalog.Application.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

public sealed class GetProductCreatorSortingsHandler : IQueryHandler<GetProductCreatorSortingsQuery, ProductCreatorSortingType[]>
{
	public Task<ProductCreatorSortingType[]> Handle(GetProductCreatorSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<ProductCreatorSortingType>()
		);
}
