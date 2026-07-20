using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Hide;

public class Tests : Data.Notifications.BaseUnitTests
{
	[Test]
	public async Task Hide_ShouldSucceed_WhenUnread()
	{
		Notification notification = CreateNotification();

		notification.Hide();

		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Hidden);
	}

	[Test]
	public async Task Hide_ShouldSucceed_WhenRead()
	{
		Notification notification = CreateNotification();
		notification.Read();

		notification.Hide();

		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Hidden);
	}

	[Test]
	public async Task Hide_ShouldSucceed_WhenOpened()
	{
		Notification notification = CreateNotification();
		notification.Read();
		notification.Open();

		notification.Hide();

		await Assert.That(notification.Status).IsEqualTo(NotificationStatus.Hidden);
	}

	[Test]
	public void Hide_ShouldFail_WhenHidden()
	{
		Assert.Throws<CustomValidationException<Notification>>(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Hide();
		});
	}
}
