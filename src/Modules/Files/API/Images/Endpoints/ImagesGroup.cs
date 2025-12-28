namespace CustomCADs.Modules.Files.API.Images.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class ImagesGroup : Group
{
	public ImagesGroup()
	{
		Configure(Paths.Images, x =>
		{
			x.Roles(Customer, Contributor, Designer, Admin);
			x.Description(x => x.WithTags(Tags[Paths.Images]));
		});
	}
}
