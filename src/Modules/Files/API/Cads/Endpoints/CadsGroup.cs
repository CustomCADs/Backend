namespace CustomCADs.Files.API.Cads.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

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
