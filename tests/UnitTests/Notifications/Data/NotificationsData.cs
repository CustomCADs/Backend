using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;

namespace CustomCADs.UnitTests.Notifications.Data;

using static NotificationsConstants;

public static class NotificationsData
{
	public static readonly string MaxValidType = new('a', TypeMaxLength - 1);
	public static readonly string MinValidType = new('a', TypeMinLength + 1);
	public static readonly string InvalidType = string.Empty;
	public static readonly string MaxInvalidType = new('a', TypeMaxLength + 1);
	public static readonly string MinInvalidType = new('a', TypeMinLength - 1);

	public static readonly string MaxValidDescription = new('a', DescriptionMaxLength - 1);
	public static readonly string MinValidDescription = new('a', DescriptionMinLength + 1);
	public static readonly string InvalidDescription = string.Empty;
	public static readonly string MaxInvalidDescription = new('a', DescriptionMaxLength + 1);
	public static readonly string MinInvalidDescription = new('a', DescriptionMinLength - 1);

	public static readonly string? ValidLink = null;

	public static readonly NotificationId ValidId = NotificationId.New();
	public static readonly AccountId ValidAuthorId = AccountId.New();
	public static readonly AccountId ValidReceiverId = AccountId.New();
}

