namespace CustomCADs.Modules.Files.API.Cads.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class CadsGroup : Group
{
	public CadsGroup()
	{
		Configure(Paths.Cads, x =>
		{
			x.Roles(CustomerRole, ContributorRole, DesignerRole, AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Cads]));
		});
	}
}
