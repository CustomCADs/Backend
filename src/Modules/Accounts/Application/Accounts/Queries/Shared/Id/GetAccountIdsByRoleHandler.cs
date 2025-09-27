using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Id;

public class GetAccountIdsByRoleHandler(IAccountReads reads)
	: IQueryHandler<GetAccountIdsByRoleQuery, ICollection<AccountId>>
{
	public async Task<ICollection<AccountId>> Handle(GetAccountIdsByRoleQuery req, CancellationToken ct = default)
		=> await reads.AllIdsByRoleAsync(req.Role, ct: ct).ConfigureAwait(false);
}
