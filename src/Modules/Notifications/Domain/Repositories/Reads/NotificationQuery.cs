using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Shared.Domain.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Domain.Repositories.Reads;

public record NotificationQuery(
	Pagination Pagination,
	AccountId? ReceiverId = null,
	NotificationStatus? Status = null,
	NotificationSorting? Sorting = null
);
