using CustomCADs.Modules.Catalog.Application.Categories.Dtos;
using CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetAll;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Internal.GetAll;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly GetAllCategoriesHandler handler;
	private readonly GetAllCategoriesQuery request = new();

	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	private readonly Category[] categories = [
		CreateCategory(ValidName, ValidDescription, CategoryId.New(1)),
		CreateCategory(MinValidName, MinValidDescription, CategoryId.New(2)),
		CreateCategory(MaxValidName, MaxValidDescription, CategoryId.New(3))
	];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Category>>>>())).ReturnsAsync(categories);
		reads.Setup(x => x.AllAsync(false, ct)).ReturnsAsync(categories);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		IEnumerable<CategoryReadDto> categories = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(categories.Select(r => r.Id), this.categories.Select(r => r.Id));
	}
}
