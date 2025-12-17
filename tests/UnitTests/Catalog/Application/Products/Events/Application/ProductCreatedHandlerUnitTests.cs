using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application;

using static ProductsData;

public class ProductCreatedHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly ProductCreatedHandler handler;
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly TagId[] TagIds = [];

	public ProductCreatedHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductCreatedApplicationEvent ae = new(ValidId, TagIds);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		writes.Verify(
			x => x.AddTagAsync(ValidId, It.Is<TagId>(x => TagIds.Contains(x)), ct),
			Times.Exactly(TagIds.Length)
		);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
