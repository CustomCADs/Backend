using CustomCADs.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;

public sealed class GetAccountSortingsHandler : IQueryHandler<GetAccountSortingsQuery, AccountSortingType[]>
{
	public Task<AccountSortingType[]> Handle(GetAccountSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<AccountSortingType>()
		);
}
