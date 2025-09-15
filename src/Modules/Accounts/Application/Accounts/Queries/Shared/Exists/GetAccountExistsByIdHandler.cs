using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Exists;

public sealed class GetAccountExistsByIdHandler(IAccountReads reads)
	: IQueryHandler<GetAccountExistsByIdQuery, bool>
{
	public async Task<bool> Handle(GetAccountExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
}
