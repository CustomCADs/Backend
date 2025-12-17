using CustomCADs.Modules.Accounts.API.Accounts.Dtos;
using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetById;

namespace CustomCADs.Modules.Accounts.API.Accounts;

internal static class Mapper
{
	extension(GetAccountByIdDto account)
	{
		internal AccountResponse ToResponse()
			=> new(
				Role: account.Role,
				Username: account.Username,
				Email: account.Email,
				FirstName: account.FirstName,
				LastName: account.LastName,
				CreatedAt: account.CreatedAt
			);
	}

}
