using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses;

public class NotificationOpenUnitTests : NotificationsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Notification>> expectValidationException
		= Assert.Throws<CustomValidationException<Notification>>;

	[Fact]
	public void Open_ShouldSucceed_WhenRead()
	{
		Notification notification = CreateNotification();
		notification.Read();

		notification.Open();

		Assert.Equal(NotificationStatus.Opened, notification.Status);
	}

	[Fact]
	public void Open_ShouldFail_WhenUnread()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();

			notification.Open();
		});
	}

	[Fact]
	public void Open_ShouldFail_WhenOpened()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Open();
		});
	}

	[Fact]
	public void Open_ShouldFail_WhenHidden()
	{
		expectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Open();
		});
	}
}
