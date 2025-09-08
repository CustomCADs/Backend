using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal;

public record GetAllNotificationsQuery(
	Pagination Pagination,
	AccountId ReceiverId,
	NotificationSorting? Sorting = null
) : IQuery<Result<GetAllNotificationsDto>>;
