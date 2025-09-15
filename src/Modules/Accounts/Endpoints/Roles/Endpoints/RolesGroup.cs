namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

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
