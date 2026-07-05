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
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();

			notification.Unhide();
		});
	}

	[Test]
	public void Hide_ShouldFail_WhenRead()
	{
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();

			notification.Unhide();
		});
	}

	[Test]
	public void Hide_ShouldFail_WhenOpened()
	{
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Open();

			notification.Unhide();
		});
	}
}