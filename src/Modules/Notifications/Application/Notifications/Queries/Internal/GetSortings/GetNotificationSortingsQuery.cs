using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetSortings;

public sealed record GetNotificationSortingsQuery : IQuery<NotificationSortingType[]>;
