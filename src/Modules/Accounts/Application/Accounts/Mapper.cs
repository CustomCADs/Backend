using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetById;

namespace CustomCADs.Modules.Accounts.Application.Accounts;

internal static class Mapper
{
	extension(Account account)
	{
		internal GetAllAccountsDto ToGetAllDto()
			=> new(
				Id: account.Id,
				Role: account.RoleName,
				Username: account.Username,
				Email: account.Email,
				FirstName: account.FirstName,
				LastName: account.LastName,
				CreatedAt: account.CreatedAt
			);

		internal GetAccountByIdDto ToGetByUsernameDto()
			=> new(
				Id: account.Id,
				Role: account.RoleName,
				Username: account.Username,
				Email: account.Email,
				FirstName: account.FirstName,
				LastName: account.LastName,
				CreatedAt: account.CreatedAt
			);
	}

}
