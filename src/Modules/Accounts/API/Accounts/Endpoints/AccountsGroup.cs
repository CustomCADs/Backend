namespace CustomCADs.Modules.Accounts.API.Accounts.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class AccountsGroup : Group
{
	public AccountsGroup()
	{
		Configure(Paths.Accounts, x =>
		{
			x.Roles(Admin);
			x.Description(opt => opt.WithTags(Tags[Paths.Accounts]));
		});
	}
}
