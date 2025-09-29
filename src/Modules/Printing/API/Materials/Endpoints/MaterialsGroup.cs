namespace CustomCADs.Printing.API.Materials.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

public class MaterialsGroup : Group
{
	public MaterialsGroup()
	{
		Configure(Paths.Materials, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[Paths.Materials]));
		});
	}
}
