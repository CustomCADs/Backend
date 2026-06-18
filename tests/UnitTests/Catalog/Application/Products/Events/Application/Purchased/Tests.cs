using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductPurchased;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application.Purchased;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly ProductsPurchasedHandler handler;
	private readonly ProductsPurchasedApplicationEvent request = new(Ids);

	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly ProductId[] Ids = [];

	public Tests()
	{
		handler = new(uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		uow.Verify(
			x => x.AddProductsPurchasesAsync(Ids, 1, ct),
			Times.Once()
		);
	}
}
