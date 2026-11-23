using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.Count;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Modules.Notifications.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.Count;

using static Data.Notifications.TestData;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly CountNotificationsHandler handler;
	private readonly CountNotificationsQuery request = new(ValidReceiverId);

	private readonly Mock<INotificationReads> reads = new();

	private readonly static Dictionary<NotificationStatus, int> expected = new()
	{
		[NotificationStatus.Unread] = 1,
		[NotificationStatus.Read] = 2,
		[NotificationStatus.Opened] = 3,
		[NotificationStatus.Hidden] = 4,
	};

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountByStatusAsync(ValidReceiverId, ct))
			.ReturnsAsync(expected);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.CountByStatusAsync(ValidReceiverId, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CountNotificationsDto counts = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(counts.Unread).IsEqualTo(expected[NotificationStatus.Unread]);
			await Assert.That(counts.Read).IsEqualTo(expected[NotificationStatus.Read]);
			await Assert.That(counts.Opened).IsEqualTo(expected[NotificationStatus.Opened]);
			await Assert.That(counts.Hidden).IsEqualTo(expected[NotificationStatus.Hidden]);
		}
	}
}
