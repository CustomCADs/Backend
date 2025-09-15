using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;

public sealed class GetAllAccountsHandler(IAccountReads reads)
	: IQueryHandler<GetAllAccountsQuery, Result<GetAllAccountsDto>>
{
	public async Task<Result<GetAllAccountsDto>> Handle(GetAllAccountsQuery req, CancellationToken ct)
	{
		Result<Account> result = await reads.AllAsync(
			query: new(
				Role: req.Role,
				Username: req.Username,
				Email: req.Email,
				FirstName: req.FirstName,
				LastName: req.LastName,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto());
	}
}
