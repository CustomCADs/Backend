using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Unhide;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	public async Task Hide_ShouldSucceed_WhenHidden()
	{
		Notification notification = CreateNotification();
		notification.Read();
		notification.Hide();

		notification.Unhide();

		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Unread);
	}

	[Test]
	public void Hide_ShouldFail_WhenUnread()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();

			notification.Unhide();
		});
	}

	[Test]
	public void Hide_ShouldFail_WhenRead()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();

			notification.Unhide();
		});
	}

	[Test]
	public void Hide_ShouldFail_WhenOpened()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Unhide();
		});
	}
}
