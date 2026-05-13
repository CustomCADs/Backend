namespace CustomCADs.Modules.Accounts.API.Accounts.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class AccountsGroup : Group
{
	public AccountsGroup()
	{
		Configure(Paths.Accounts, x =>
		{
			x.Roles(AdminRole);
			x.Description(opt => opt.WithTags(Tags[Paths.Accounts]));
		});
	}
}
