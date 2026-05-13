using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Exists;

public sealed class GetAccountExistsByUsernameHandler(IAccountReads reads)
	: IQueryHandler<GetAccountExistsByUsernameQuery, bool>
{
	public async Task<bool> Handle(GetAccountExistsByUsernameQuery req, CancellationToken ct)
		=> await reads.ExistsByUsernameAsync(req.Username, ct).ConfigureAwait(false);
}
