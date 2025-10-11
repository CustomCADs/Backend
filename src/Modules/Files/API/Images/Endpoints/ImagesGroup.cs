namespace CustomCADs.Files.API.Images.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

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
