using CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetSortings;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Application.Notifications.Queries.Internal.GetSortings;

public class Tests : Data.Notifications.BaseUnitTests
{
	private readonly GetNotificationSortingsHandler handler = new();
	private readonly GetNotificationSortingsQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		NotificationSortingType[] sortings = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<NotificationSortingType>());
	}
}
