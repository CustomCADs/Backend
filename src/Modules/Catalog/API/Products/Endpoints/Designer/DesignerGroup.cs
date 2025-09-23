namespace CustomCADs.Catalog.API.Products.Endpoints.Designer;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class DesignerGroup : SubGroup<ProductsGroup>
{
	public DesignerGroup()
	{
		Configure(Paths.Designer, x =>
		{
			x.Roles(Designer);
			x.Description(x => x.WithTags(Tags[$"{Paths.Products}/{Paths.Designer}"]));
		});
	}
}
