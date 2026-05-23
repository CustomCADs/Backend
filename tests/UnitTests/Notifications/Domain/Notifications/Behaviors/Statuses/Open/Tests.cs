using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Open;

public class Tests : Data.Notifications.BaseUnitTests
{
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
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();

			notification.Open();
		});
	}

	[Fact]
	public void Open_ShouldFail_WhenOpened()
	{
		ExpectValidationException(() =>
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
		ExpectValidationException(() =>
		{
			Notification notification = CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Open();
		});
	}
}
