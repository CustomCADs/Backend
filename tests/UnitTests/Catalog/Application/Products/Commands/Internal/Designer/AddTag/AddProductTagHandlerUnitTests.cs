using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.AddTag;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Designer.AddTag;

using static ProductsData;

public class AddProductTagHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly AddProductTagHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly static ProductId id = ValidId;
	private readonly static TagId tagId = TagId.New();
	private readonly static AccountId callerId = AccountId.New();

	public AddProductTagHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(CreateProduct());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		AddProductTagCommand command = new(id, tagId, callerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		AddProductTagCommand command = new(id, tagId, callerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddTagAsync(id, tagId, ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		AddProductTagCommand command = new(id, tagId, callerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.ProductTagAdded)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Product);
		AddProductTagCommand command = new(id, tagId, callerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
