using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetStatuses;

public record GetNotificationStatusesQuery : IQuery<NotificationStatus[]>;
