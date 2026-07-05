using CustomCADs.Modules.Catalog.Application.Categories.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Shared.BatchGetById;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly BatchGetCategoryByIdHandler handler;
	private readonly BatchGetCategorByIdQuery request = new(Ids);

	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private static readonly CategoryId[] Ids = [
		CategoryId.New(1),
		CategoryId.New(2),
		CategoryId.New(3)
	];
	private static readonly Category[] Categories = [.. Ids.Select(id => CreateCategory(id: id))];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);
		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>())).ReturnsAsync(Categories);
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var actualCategories = (await handler.Handle(request, ct)).Select(x => (x.Key, x.Value));

		// Assert
		await Assert.That([.. Categories.Select(c => (c.Id, c.Name))]).IsEquivalentTo(actualCategories);
	}
}
