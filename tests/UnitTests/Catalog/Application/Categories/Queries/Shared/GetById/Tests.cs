using CustomCADs.Modules.Catalog.Application.Categories.Queries.Shared;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Queries.Shared.GetById;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly GetCategoryNameByIdHandler handler;
	private readonly GetCategoryNameByIdQuery request = new(ValidId);

	private readonly Mock<ICategoryReads> reads = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()))
			.ReturnsAsync(CreateCategory(name: ValidName));
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Category>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		string result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidName, result);
	}
}
