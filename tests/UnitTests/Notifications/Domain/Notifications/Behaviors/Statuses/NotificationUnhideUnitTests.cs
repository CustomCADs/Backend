using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses;

public class NotificationUnhideUnitTests : NotificationsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Notification>> expectValidationException
		= Assert.Throws<CustomValidationException<Notification>>;

	[Fact]
	public void Hide_ShouldSucceed_WhenHidden()
	{
		Notification notification = CreateNotification();
		notification.Read();
		notification.Hide();

		notification.Unhide();

		Assert.Equal(NotificationStatus.Unread, notification.Status);
	}

	[Fact]
	public void Hide_ShouldFail_WhenUnread()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();

			notification.Unhide();
		});
	}

	[Fact]
	public void Hide_ShouldFail_WhenRead()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();

			notification.Unhide();
		});
	}

	[Fact]
	public void Hide_ShouldFail_WhenOpened()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Unhide();
		});
	}
}
