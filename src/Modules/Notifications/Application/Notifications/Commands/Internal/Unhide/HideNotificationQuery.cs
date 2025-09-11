using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Unhide;

public record UnhideNotificationQuery(NotificationId Id, AccountId CallerId) : ICommand;
