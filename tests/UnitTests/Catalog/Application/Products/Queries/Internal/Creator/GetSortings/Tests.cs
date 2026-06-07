using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GetProductCreatorSortingsHandler handler = new();
	private readonly GetProductCreatorSortingsQuery reqeust = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ProductCreatorSortingType[] sortings = await handler.Handle(reqeust, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<ProductCreatorSortingType>());
	}
}
