using CustomCADs.Modules.Catalog.Application.Categories.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Shared.GetExists;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly GetCategoryExistsByIdHandler handler;
	private readonly GetCategoryExistsByIdQuery request = new(ValidId);

	private readonly Mock<ICategoryReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);
	}

	[Fact]
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

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(ValidId, ct)).ReturnsAsync(exists);

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
