namespace CustomCADs.Modules.Accounts.API.Roles.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class RolesGroup : Group
{
	public RolesGroup()
	{
		Configure(Paths.Roles, x =>
		{
			x.Roles(Admin);
			x.Description(opt => opt.WithTags(Tags[Paths.Roles]));
		});
	}
}
