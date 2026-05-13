namespace CustomCADs.Modules.Accounts.API.Roles.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class RolesGroup : Group
{
	public RolesGroup()
	{
		Configure(Paths.Roles, x =>
		{
			x.Roles(AdminRole);
			x.Description(opt => opt.WithTags(Tags[Paths.Roles]));
		});
	}
}
