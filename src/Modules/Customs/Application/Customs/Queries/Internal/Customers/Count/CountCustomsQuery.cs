using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.Count;

public sealed record CountCustomsQuery(
	AccountId CallerId
) : IQuery<CountCustomsDto>;
