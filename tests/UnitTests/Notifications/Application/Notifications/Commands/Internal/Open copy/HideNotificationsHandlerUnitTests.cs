using CustomCADs.Notifications.Application.Notifications.Commands.Internal.Hide;
using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Commands.Internal.Hide;

using static NotificationsData;

public class HideNotificationsHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly HideNotificationHandler handler;
	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public HideNotificationsHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateNotification().Read().Open());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		HideNotificationCommand command = new(ValidId, ValidReceiverId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		HideNotificationCommand command = new(ValidId, ValidReceiverId);

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
		HideNotificationCommand command = new(ValidId, ValidReceiverId);

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
		HideNotificationCommand command = new(ValidId, ValidReceiverId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Notification>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
