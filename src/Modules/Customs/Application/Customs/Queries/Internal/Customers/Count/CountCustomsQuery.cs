using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

public sealed record CountCustomsQuery(
	AccountId CallerId
) : IQuery<CountCustomsDto>;
