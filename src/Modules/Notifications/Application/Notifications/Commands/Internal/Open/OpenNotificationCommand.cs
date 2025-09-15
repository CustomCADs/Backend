using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Open;

public sealed record OpenNotificationCommand(NotificationId Id, AccountId CallerId) : ICommand;
