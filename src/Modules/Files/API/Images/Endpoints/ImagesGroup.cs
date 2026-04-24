namespace CustomCADs.Modules.Files.API.Images.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class ImagesGroup : Group
{
	public ImagesGroup()
	{
		Configure(Paths.Images, x =>
		{
			x.Roles(CustomerRole, ContributorRole, DesignerRole, AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Images]));
		});
	}
}
