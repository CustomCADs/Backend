using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Admin.Remove;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Admin.Remove;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly RemoveCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Custom custom = CreateCustom();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);

		custom.Report();
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: ValidAdminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: ValidAdminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: ValidAdminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomRemoved)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: ValidAdminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(CustomStatus.Removed, custom.CustomStatus);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: ValidAdminId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.Handle(command, ct)
		);
	}
}
