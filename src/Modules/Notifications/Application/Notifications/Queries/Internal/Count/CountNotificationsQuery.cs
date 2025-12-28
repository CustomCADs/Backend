using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Notifications.Application.Notifications.Queries.Internal.Count;

public sealed record CountNotificationsQuery(
	AccountId CallerId
) : IQuery<CountNotificationsDto>;
