namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class DesignerGroup : Group
{
	public DesignerGroup()
	{
		Configure(Paths.ProductsDesigner, ep =>
		{
			ep.Roles(Designer);
			ep.Description(d => d.WithTags(Tags[Paths.ProductsDesigner]));
		});
	}
}
