namespace CustomCADs.Catalog.API.Tags.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class TagGroup : Group
{
	public TagGroup()
	{
		Configure(Paths.Tags, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[Paths.Tags]));
		});
	}
}
