using CustomCADs.Modules.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetExists;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly GetProductExistsByIdHandler handler;
	private readonly GetProductExistsByIdQuery request = new(ValidId);

	private readonly Mock<IProductReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(true);

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.ExistsByIdAsync(ValidId, ct),
			Times.Once()
		);
	}

	[Test]
	[Arguments(true)]
	[Arguments(false)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEqualTo(exists);
	}
}