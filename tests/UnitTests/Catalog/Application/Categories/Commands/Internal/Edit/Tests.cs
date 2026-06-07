using CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Edit;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Commands.Internal.Edit;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly EditCategoryHandler handler;
	private readonly EditCategoryCommand request = new(ValidId, Dto: new(ValidName, ValidDescription));

	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private readonly Category category = CreateCategory();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(category);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

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

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldModifyCategory()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.Equal(ValidName, category.Name);
			Assert.Equal(ValidDescription, category.Description);
		});
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(
				ValidId,
				category
			),
			Times.Once()
		);
	}
}
