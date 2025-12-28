using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Commands.Internal.Unhide;

public sealed record UnhideNotificationCommand(NotificationId Id, AccountId CallerId) : ICommand;
