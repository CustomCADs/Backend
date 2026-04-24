namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class TagGroup : Group
{
	public TagGroup()
	{
		Configure(Paths.Tags, x =>
		{
			x.Roles(AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Tags]));
		});
	}
}
