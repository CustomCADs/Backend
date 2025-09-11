using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.ActiveCarts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit;

using static ProductsData;

public class EditProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly EditProductHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Product product = CreateProductWithId();
	private readonly AccountId[] receiverIds = [ValidDesignerId, ValidCreatorId];

	public EditProductHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(true);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountsWithProductInCartQuery>(x => x.ProductId == ValidId),
			ct
		)).ReturnsAsync(receiverIds);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountsWithProductInCartQuery>(x => x.ProductId == ValidId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.ReceiverIds == receiverIds)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(false);

		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);

		EditProductCommand command = new(
			Id: ValidId,
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			CategoryId: ValidCategoryId,
			CreatorId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
