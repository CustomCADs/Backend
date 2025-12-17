namespace CustomCADs.Modules.Notifications.Domain.Notifications;

using static NotificationsConstants;

internal static class Validations
{
	extension(Notification notification)
	{
		internal Notification ValidateType()
			=> notification
				.ThrowIfNull(
					expression: (x) => x.Type,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: (x) => x.Type,
					length: (TypeMinLength, TypeMaxLength)
				);

		internal Notification ValidateContent()
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

}
