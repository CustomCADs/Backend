namespace CustomCADs.Accounts.API.Accounts.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

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
