using CustomCADs.Notifications.Application.Notifications.Events;
using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Notifications.Domain.Repositories;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Events;

using static NotificationsData;

public class NotificationRequestedHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly NotificationRequestedHandler handler;
	private readonly Mock<IWrites<Notification>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly NotificationId[] ids = [];

	public NotificationRequestedHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		NotificationRequestedEvent ae = new(
			Type: NotificationType.Unkown,
			Description: MinValidDescription,
			Link: ValidLink,
			AuthorId: ValidAuthorId,
			ReceiverId: ValidReceiverId
		);

		// Act
		await handler.Handle(ae);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Notification>(x =>
				x.Type == ae.Type.ToString()
				&& x.Content == new NotificationContent(ae.Description, ae.Link)
				&& x.AuthorId == ae.AuthorId
				&& x.ReceiverId == ae.ReceiverId
			),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
