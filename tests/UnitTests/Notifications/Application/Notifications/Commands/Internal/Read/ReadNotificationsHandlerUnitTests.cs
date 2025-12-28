using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Read;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Commands.Internal.Read;

using static NotificationsData;

public class ReadNotificationsHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly ReadNotificationHandler handler;
	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public ReadNotificationsHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateNotification());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ReadNotificationCommand command = new(ValidId, ValidReceiverId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ReadNotificationCommand command = new(ValidId, ValidReceiverId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenCallerUnauthorized()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(CreateNotification(receiverId: AccountId.New()));
		ReadNotificationCommand command = new(ValidId, ValidReceiverId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Notification>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenNotificationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Notification);
		ReadNotificationCommand command = new(ValidId, ValidReceiverId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Notification>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
