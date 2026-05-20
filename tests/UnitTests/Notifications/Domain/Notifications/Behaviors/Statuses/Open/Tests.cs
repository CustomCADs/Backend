using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Open;

public class Tests : Data.Notifications.BaseUnitTests
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
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();

			notification.Open();
		}));
	}

	[Fact]
	public void Open_ShouldFail_WhenOpened()
	{
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();
			notification.Read();
			notification.Open();

			notification.Open();
		}));
	}

	[Fact]
	public void Open_ShouldFail_WhenHidden()
	{
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Open();
		}));
	}
}
