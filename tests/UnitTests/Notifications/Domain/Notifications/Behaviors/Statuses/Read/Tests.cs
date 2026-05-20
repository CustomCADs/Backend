using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.UnitTests.Notifications.Domain.Notifications.Behaviors.Statuses.Read;

public class Tests : Data.Notifications.BaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Notification>> expectValidationException
		= Assert.Throws<CustomValidationException<Notification>>;

	[Fact]
	public void Read_ShouldSucceed_WhenUnread()
	{
		Notification notification = CreateNotification();
		notification.Read();
		Assert.Equal(NotificationStatus.Read, notification.Status);
	}

	[Fact]
	public void Read_ShouldFail_WhenRead()
	{
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();
			notification.Read();

			notification.Read();
		}));
	}

	[Fact]
	public void Read_ShouldFail_WhenOpened()
	{
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();
			notification.Read();
			notification.Open();

			notification.Read();
		}));
	}

	[Fact]
	public void Read_ShouldFail_WhenHidden()
	{
		expectValidationException((Action)(() =>
		{
			Notification notification = Data.Notifications.BaseUnitTests.CreateNotification();
			notification.Read();
			notification.Hide();

			notification.Read();
		}));
	}
}
