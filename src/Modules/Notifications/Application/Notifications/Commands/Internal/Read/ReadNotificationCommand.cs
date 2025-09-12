using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Commands.Internal.Read;

public record ReadNotificationCommand(NotificationId Id, AccountId CallerId) : ICommand;
