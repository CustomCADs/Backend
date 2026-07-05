using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add;

using static Data.ActiveCarts.TestData;

public class Tests : Data.ActiveCarts.BaseUnitTests
{
	private readonly AddActiveCartItemHandler handler;
	private static AddActiveCartItemCommand Request(CustomizationId? customizationId)
		=> new(
			CallerId: ValidBuyerId,
			CustomizationId: customizationId,
			ForDelivery: customizationId is not null,
			ProductId: ValidProductId
		);

	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IWrites<ActiveCartItem>> writes = new();
	private readonly Mock<IRequestSender> sender = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetProductExistsByIdQuery>(),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationExistsByIdQuery>(),
			ct
		)).ReturnsAsync(true);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Handle_ShouldPersistToDatabase(CustomizationId? customizationId)
	{
		// Arrange

		// Act
		await handler.Handle(Request(customizationId), ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Handle_ShouldSendRequests(CustomizationId? customizationId)
	{
		// Arrange

		// Act
		await handler.Handle(Request(customizationId), ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetProductExistsByIdQuery>(x => x.Id == ValidProductId),
				ct
			),
			Times.Once()
		);

		if (customizationId is not null)
		{
			sender.Verify(
				x => x.SendQueryAsync(
					It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == customizationId),
					ct
				),
				Times.Once()
			);
		}
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Handle_ShouldThrowException_WhenProductNotFound(CustomizationId? customizationId)
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetProductExistsByIdQuery>(x => x.Id == ValidProductId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(() => handler.Handle(Request(customizationId), ct));
	}
}
