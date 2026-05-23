using CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Open;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Commands.Internal.Open;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly OpenNotificationHandler handler;
	private readonly OpenNotificationCommand request = new(ValidId, ValidReceiverId);

	private readonly Mock<INotificationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateNotification().Read());
	}

	[Fact]
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

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldThrow_WhenCallerUnauthorizedAccess()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(CreateNotification(receiverId: AccountId.New()));

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Notification>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrow_WhenNotificationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Notification);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Notification>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
