using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Read;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Commands.Internal.Read;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly ReadNotificationHandler handler;
	private readonly ReadNotificationCommand request = new(ValidId, ValidReceiverId);

	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateNotification());
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrow_WhenCallerUnauthorizedAccess()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(CreateNotification(receiverId: AccountId.New()));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Notification>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrow_WhenNotificationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Notification);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Notification>>(() => handler.Handle(request, ct));
	}
}