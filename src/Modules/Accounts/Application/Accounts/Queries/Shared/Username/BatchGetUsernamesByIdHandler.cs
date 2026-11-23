using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Username;

public sealed class BatchGetUsernamesByIdHandler(IAccountReads reads)
	: IQueryHandler<BatchGetUsernamesByIdQuery, Dictionary<AccountId, string>>
{
	public async Task<Dictionary<AccountId, string>> Handle(BatchGetUsernamesByIdQuery req, CancellationToken ct)
	{
		AccountQuery query = new(
			Ids: req.Ids,
			Pagination: new(Limit: req.Ids.Length)
		);
		Result<Account> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

		return result.Items.ToDictionary(x => x.Id, x => x.Username);
	}
}
