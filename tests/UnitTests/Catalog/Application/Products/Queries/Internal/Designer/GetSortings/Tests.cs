using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GetProductDesignerSortingsHandler handler = new();
	private readonly GetProductDesignerSortingsQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ProductDesignerSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<ProductDesignerSortingType>());
	}
}
