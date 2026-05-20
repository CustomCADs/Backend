using CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly ProductDeletedHandler handler;
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldBulkDelete_WhenThresholdReached()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: ValidProductId,
			ImageId: default,
			CadId: default
		);

		// Act
		await handler.HandleAsync(ie);

		// Assert
		uow.Verify(
			x => x.BulkDeleteItemsByProductIdAsync(ValidProductId, ct),
			Times.Once()
		);
	}
}
