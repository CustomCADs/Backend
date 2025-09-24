using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;
using CustomCADs.Accounts.API.Accounts.Dtos;

namespace CustomCADs.Accounts.API.Accounts;

internal static class Mapper
{
	internal static AccountResponse ToResponse(this GetAccountByIdDto account)
		=> new(
			Role: account.Role,
			Username: account.Username,
			Email: account.Email,
			FirstName: account.FirstName,
			LastName: account.LastName,
			CreatedAt: account.CreatedAt
		);
}
