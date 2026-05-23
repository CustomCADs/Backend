using CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Create;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Categories.Commands.Internal.Create;

using static Data.Categories.TestData;

public class Tests : Data.Categories.BaseUnitTests
{
	private readonly CreateCategoryHandler handler;
	private readonly CreateCategoryCommand request = new(Dto: new(ValidName, ValidDescription));

	private readonly Mock<ICategoryWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CategoryId, Category>> cache = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription),
			ct
		)).ReturnsAsync(CreateCategory(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
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
				It.Is<Category>(x => x.Name == ValidName && x.Description == ValidDescription)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CategoryId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
