using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.Count;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.Count;

using static NotificationsData;

public class CountNotificationsHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly CountNotificationsHandler handler;
	private readonly Mock<INotificationReads> reads = new();

	private readonly static Dictionary<NotificationStatus, int> expected = new()
	{
		[NotificationStatus.Unread] = 1,
		[NotificationStatus.Read] = 2,
		[NotificationStatus.Opened] = 3,
		[NotificationStatus.Hidden] = 4,
	};

	public CountNotificationsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountByStatusAsync(ValidReceiverId, ct))
			.ReturnsAsync(expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CountNotificationsQuery query = new(ValidReceiverId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountByStatusAsync(ValidReceiverId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CountNotificationsQuery query = new(ValidReceiverId);

		// Act
		CountNotificationsDto counts = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(expected[NotificationStatus.Unread], counts.Unread),
			() => Assert.Equal(expected[NotificationStatus.Read], counts.Read),
			() => Assert.Equal(expected[NotificationStatus.Opened], counts.Opened),
			() => Assert.Equal(expected[NotificationStatus.Hidden], counts.Hidden)
		);
	}
}
