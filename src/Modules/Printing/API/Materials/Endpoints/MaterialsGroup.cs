namespace CustomCADs.Modules.Printing.API.Materials.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class MaterialsGroup : Group
{
	public MaterialsGroup()
	{
		Configure(Paths.Materials, x =>
		{
			x.Roles(AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Materials]));
		});
	}
}
