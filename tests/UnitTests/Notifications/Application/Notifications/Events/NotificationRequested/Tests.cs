using CustomCADs.Modules.Notifications.Application.Contracts;
using CustomCADs.Modules.Notifications.Application.Notifications.Events;
using CustomCADs.Modules.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Modules.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Events.NotificationRequested;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly NotificationRequestedHandler handler;
	private readonly NotificationRequestedEvent request = new(
		Type: NotificationType.Unkown,
		Description: MinValidDescription,
		Link: ValidLink,
		AuthorId: ValidAuthorId,
		ReceiverIds: []
	);

	private readonly Mock<IWrites<Notification>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<INotificationsRealTimeNotifier> notifier = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, notifier.Object);

		Notification[] notifications = [CreateNotification(), CreateNotification()];
		writes.Setup(x => x.AddAsync(
			It.Is<Notification>(x => x.ReceiverId == ValidReceiverId),
			ct
		)).ReturnsAsync(notifications.First());
		uow.Setup(x => x.InsertNotificationsAsync(
			It.IsAny<Notification[]>(),
			ct
		)).ReturnsAsync(notifications);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidAuthorId),
			ct
		)).ReturnsAsync("John_User");
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenSingleReceiver()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request with { ReceiverIds = [ValidReceiverId] });

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Notification>(x =>
					x.Type == request.Type.ToString()
					&& x.Content == new NotificationContent(request.Description, request.Link)
					&& x.AuthorId == request.AuthorId
					&& x.ReceiverId == ValidReceiverId
				),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldBulkInsert_WhenMultipleReceivers()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request with { ReceiverIds = [ValidReceiverId, ValidAuthorId] });

		// Assert
		uow.Verify(
			x => x.InsertNotificationsAsync(
				It.IsAny<Notification[]>(),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidAuthorId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldNotifySubscribers()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		notifier.Verify(
			x => x.NotifyUsersAsync(
				request.ReceiverIds,
				It.IsAny<string>(),
				It.IsAny<object>(),
				ct
			),
			Times.Once()
		);
	}
}
