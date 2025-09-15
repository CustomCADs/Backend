using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Hide;

public sealed record HideNotificationCommand(NotificationId Id, AccountId CallerId) : ICommand;
