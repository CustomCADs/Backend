using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GetProductGallerySortingsHandler handler = new();
	private readonly GetProductGallerySortingsQuery request = new();

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ProductGallerySortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Enum.GetValues<ProductGallerySortingType>()).IsEquivalentTo(sortings);
	}
}
