using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly RemoveProductTagHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateProduct());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RemoveProductTagCommand command = new(ValidId, ValidTagId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RemoveProductTagCommand command = new(ValidId, ValidTagId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.RemoveTagAsync(ValidId, ValidTagId), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		RemoveProductTagCommand command = new(ValidId, ValidTagId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.ProductTagRemoved)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Product);
		RemoveProductTagCommand command = new(ValidId, ValidTagId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
