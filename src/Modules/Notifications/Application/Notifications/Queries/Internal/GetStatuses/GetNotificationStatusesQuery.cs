using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public sealed record GetNotificationStatusesQuery : IQuery<NotificationStatus[]>;
