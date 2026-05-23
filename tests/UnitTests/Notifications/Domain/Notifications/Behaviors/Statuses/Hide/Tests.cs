using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Hide;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Fact]
	public void Hide_ShouldSucceed_WhenUnread()
	{
		Notification notification = CreateNotification();

		notification.Hide();

		Assert.Equal(NotificationStatus.Hidden, notification.Status);
	}

	[Fact]
	public void Hide_ShouldSucceed_WhenRead()
	{
		Notification notification = CreateNotification();
		notification.Read();

		notification.Hide();

		Assert.Equal(NotificationStatus.Hidden, notification.Status);
	}

	[Fact]
	public void Hide_ShouldSucceed_WhenOpened()
	{
		Notification notification = CreateNotification();
		notification.Read();
		notification.Open();

		notification.Hide();

		Assert.Equal(NotificationStatus.Hidden, notification.Status);
	}

	[Fact]
	public void Hide_ShouldFail_WhenHidden()
	{
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Hide();
		});
	}
}
