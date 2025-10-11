namespace CustomCADs.Files.API.Cads.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

public class CadsGroup : Group
{
	public CadsGroup()
	{
		Configure(Paths.Cads, x =>
		{
			x.Roles(Customer, Contributor, Designer, Admin);
			x.Description(x => x.WithTags(Tags[Paths.Cads]));
		});
	}
}
