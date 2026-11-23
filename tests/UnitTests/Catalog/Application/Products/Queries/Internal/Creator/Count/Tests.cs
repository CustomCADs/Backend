using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.Count;
using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.Count;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly ProductsCountHandler handler;
	private readonly ProductsCountQuery request = new(ValidCreatorId);

	private readonly Mock<IProductReads> reads = new();

	private readonly Dictionary<ProductStatus, int> dict = new()
	{
		[ProductStatus.Unchecked] = 3,
		[ProductStatus.Validated] = 2,
		[ProductStatus.Reported] = 1,
		[ProductStatus.Removed] = 0,
	};

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountByStatusAsync(ValidCreatorId, ct))
			.ReturnsAsync(dict);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.CountByStatusAsync(ValidCreatorId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(result.Unchecked).IsEqualTo(dict[ProductStatus.Unchecked]);
			await Assert.That(result.Validated).IsEqualTo(dict[ProductStatus.Validated]);
			await Assert.That(result.Reported).IsEqualTo(dict[ProductStatus.Reported]);
			await Assert.That(result.Banned).IsEqualTo(dict[ProductStatus.Removed]);
		}
	}
}
