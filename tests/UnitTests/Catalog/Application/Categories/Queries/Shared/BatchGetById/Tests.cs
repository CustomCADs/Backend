using CustomCADs.Modules.Catalog.Application.Categories.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Shared.BatchGetById;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly BatchGetCategoryByIdHandler handler;
	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private readonly static CategoryId[] ids = [
		CategoryId.New(1),
		CategoryId.New(2),
		CategoryId.New(3)
	];
	private static readonly Category[] categories = [.. ids.Select(id => CreateCategory(id: id))];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);
		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>())).ReturnsAsync(categories);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		BatchGetCategorByIdQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		BatchGetCategorByIdQuery query = new(ids);

		// Act
		var actualCategories = (await handler.Handle(query, ct)).Select(x => (x.Key, x.Value));

		// Assert
		Assert.Equal(actualCategories, [.. categories.Select(c => (c.Id, c.Name))]);
	}
}
