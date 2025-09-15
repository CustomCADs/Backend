using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public sealed record GetNotificationStatusesQuery : IQuery<NotificationStatus[]>;
