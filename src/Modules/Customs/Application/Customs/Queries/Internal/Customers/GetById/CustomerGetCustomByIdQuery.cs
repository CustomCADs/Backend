using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed record CustomerGetCustomByIdQuery(
	CustomId Id,
	AccountId CallerId
) : IQuery<CustomerGetCustomByIdDto>;
