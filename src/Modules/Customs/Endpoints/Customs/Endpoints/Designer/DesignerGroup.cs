namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class DesignerGroup : Group
{
	public DesignerGroup()
	{
		Configure(Paths.CustomsDesigner, x =>
		{
			x.Roles(Designer);
			x.Description(x => x.WithTags(Tags[Paths.CustomsDesigner]));
		});
	}
}
