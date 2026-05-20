using CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetExists;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GetProductExistsByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);
		GetProductExistsByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.ExistsByIdAsync(ValidId, ct),
			Times.Once()
		);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);
		GetProductExistsByIdQuery query = new(ValidId);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
