namespace CustomCADs.Printing.Endpoints.Materials.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

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
