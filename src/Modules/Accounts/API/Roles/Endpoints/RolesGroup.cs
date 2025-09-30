namespace CustomCADs.Accounts.API.Roles.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

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
