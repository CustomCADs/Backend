using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.GetAll;

public record GetAllNotificationsQuery(
	Pagination Pagination,
	AccountId ReceiverId,
	NotificationStatus? Status = null,
	NotificationSorting? Sorting = null
) : IQuery<Result<GetAllNotificationsDto>>;
