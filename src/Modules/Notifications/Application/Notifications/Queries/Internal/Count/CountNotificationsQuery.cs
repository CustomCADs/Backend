using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Application.Notifications.Queries.Internal.Count;

public sealed record CountNotificationsQuery(
	AccountId CallerId
) : IQuery<CountNotificationsDto>;
