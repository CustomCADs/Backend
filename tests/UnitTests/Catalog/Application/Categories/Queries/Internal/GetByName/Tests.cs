using CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetByName;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Internal.GetByName;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly GetCategoryByNameHandler handler;
	private readonly GetCategoryByNameQuery request = new(ValidName);

	private readonly Mock<ICategoryReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByNameAsync(ValidName, false, ct))
			.ReturnsAsync(CreateCategory());
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByNameAsync(ValidName, false, ct),
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
		await Assert.That(result.Id).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByNameAsync(ValidName, false, ct)).ReturnsAsync(null as Category);

		// Assert
		await Assert.ThrowsAsync(() => handler.Handle(request, ct));
	}
}
