using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetStatuses;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly GetNotificationStatusesHandler handler = new();
	private readonly GetNotificationStatusesQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		NotificationStatus[] statuses = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(statuses, Enum.GetValues<NotificationStatus>());
	}
}
