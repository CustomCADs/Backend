using CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetSortings;
using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetSortings;

public class GetNotificationSortingsHandlerUnitTests : NotificationsBaseUnitTests
{
	private readonly GetNotificationSortingsHandler handler = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetNotificationSortingsQuery query = new();

		// Act
		NotificationSortingType[] sortings = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<NotificationSortingType>());
	}
}
