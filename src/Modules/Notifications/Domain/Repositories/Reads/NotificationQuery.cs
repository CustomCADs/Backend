using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Notifications.Domain.Repositories.Reads;

public record NotificationQuery(
	Pagination Pagination,
	AccountId? ReceiverId = null,
	NotificationStatus? Status = null,
	Sorting<NotificationSortingType>? Sorting = null
);
