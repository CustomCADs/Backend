namespace CustomCADs.Catalog.API.Products.Endpoints.Designer;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class DesignerGroup : Group
{
	public DesignerGroup()
	{
		Configure(Paths.ProductsDesigner, x =>
		{
			x.Roles(Designer);
			x.Description(x => x.WithTags(Tags[Paths.ProductsDesigner]));
		});
	}
}
