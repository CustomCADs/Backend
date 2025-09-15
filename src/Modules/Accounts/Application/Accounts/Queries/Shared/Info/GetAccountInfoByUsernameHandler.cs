using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Info;

public sealed class GetAccountInfoByUsernameHandler(IAccountReads reads)
	: IQueryHandler<GetAccountInfoByUsernameQuery, AccountInfoDto>
{
	public async Task<AccountInfoDto> Handle(GetAccountInfoByUsernameQuery req, CancellationToken ct = default)
	{
		Account account = await reads.SingleByUsernameAsync(req.Username, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ByProp(nameof(req.Username), req.Username);

		return new(
			CreatedAt: account.CreatedAt,
			TrackViewedProducts: account.TrackViewedProducts,
			FirstName: account.FirstName,
			LastName: account.LastName
		);
	}
}
