using CustomCADs.Modules.Accounts.API.Accounts.Dtos;
using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetAll;

namespace CustomCADs.Modules.Accounts.API.Accounts.Endpoints.Get.All;

public class GetAccountsMapper : ResponseMapper<AccountResponse, GetAllAccountsDto>
{
	public override AccountResponse FromEntity(GetAllAccountsDto account)
		=> new(
			Username: account.Username,
			Email: account.Email,
			Role: account.Role,
			FirstName: account.FirstName,
			LastName: account.LastName,
			CreatedAt: account.CreatedAt
		);
}
