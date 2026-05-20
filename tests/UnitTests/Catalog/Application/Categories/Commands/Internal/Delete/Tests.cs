using CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Delete;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Commands.Internal.Delete;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly DeleteCategoryHandler handler;
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<ICategoryWrites> writes = new();
	private readonly Mock<ICategoryReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateCategory(ValidName, ValidDescription));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(
			x => x.Remove(
				It.Is<Category>(x => x.Id == ValidId)
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldClearCache()
	{
		// Arrange
		DeleteCategoryCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}
}
