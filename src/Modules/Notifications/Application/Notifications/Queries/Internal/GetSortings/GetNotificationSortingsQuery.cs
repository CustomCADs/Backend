using CustomCADs.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetSortings;

public sealed record GetNotificationSortingsQuery : IQuery<NotificationSortingType[]>;
