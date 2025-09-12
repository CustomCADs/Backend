using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetStatuses;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public class GetNotificationStatusesHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly GetNotificationStatusesHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetNotificationStatusesQuery query = new();

		// Act
		NotificationStatus[] statuses = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(statuses, Enum.GetValues<NotificationStatus>());
	}
}
