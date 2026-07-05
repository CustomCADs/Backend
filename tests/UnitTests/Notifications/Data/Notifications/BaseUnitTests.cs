using CustomCADs.Modules.Notifications.Domain.Notifications;
using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;

namespace CustomCADs.UnitTests.Notifications.Data.Notifications;

using static TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static readonly Func<Action, CustomValidationException<Notification>> ExpectValidationException
		= Assert.Throws<CustomValidationException<Notification>>;

	public static Notification CreateNotification(
		string? type = null,
		string? description = null,
		string? link = null,
		AccountId? authorId = null,
		AccountId? receiverId = null,
		NotificationId? id = null
	) => Notification.Create(
			id: id ?? ValidId,
			type: type ?? MaxValidType,
			content: new(description ?? MaxValidDescription, link),
			authorId: authorId ?? ValidAuthorId,
			receiverId: receiverId ?? ValidReceiverId
		);
}
