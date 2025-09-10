namespace CustomCADs.Notifications.Domain.Notifications;

using static NotificationsConstants;

internal static class Validations
{
	internal static Notification ValidateType(this Notification notification)
		=> notification
			.ThrowIfNull(
				expression: (x) => x.Type,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: (x) => x.Type,
				length: (TypeMinLength, TypeMaxLength)
			);

	internal static Notification ValidateContent(this Notification notification)
		=> notification
			.ThrowIfNull(
				expression: (x) => x.Content.Description,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: (x) => x.Content.Description,
				length: (DescriptionMinLength, DescriptionMaxLength)
			);
}
