using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.GetAll;

public sealed record GetAllNotificationsQuery(
	Pagination Pagination,
	AccountId CallerId,
	NotificationStatus? Status = null,
	Sorting<NotificationSortingType>? Sorting = null
) : IQuery<Result<GetAllNotificationsDto>>;
