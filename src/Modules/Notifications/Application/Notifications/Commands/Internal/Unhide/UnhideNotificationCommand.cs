using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Unhide;

public sealed record UnhideNotificationCommand(NotificationId Id, AccountId CallerId) : ICommand;
