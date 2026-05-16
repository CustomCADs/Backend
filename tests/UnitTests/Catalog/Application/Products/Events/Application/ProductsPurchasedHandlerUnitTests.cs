using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductPurchased;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application;

public class ProductsPurchasedHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly ProductsPurchasedHandler handler;
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly ProductId[] ids = [];

	public ProductsPurchasedHandlerUnitTests()
	{
		handler = new(uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductsPurchasedApplicationEvent @event = new(ids);

		// Act
		await handler.HandleAsync(@event, ct);

		// Assert
		uow.Verify(x => x.AddProductsPurchasesAsync(ids, 1, ct), Times.Once());
	}
}
