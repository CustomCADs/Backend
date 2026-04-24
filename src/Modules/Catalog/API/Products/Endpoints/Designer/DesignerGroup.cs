namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer;

using static APIConstants;
using static DomainConstants.Users;

public class DesignerGroup : SubGroup<ProductsGroup>
{
	public DesignerGroup()
	{
		Configure(Paths.Designer, x =>
		{
			x.Roles(DesignerRole);
			x.Description(x => x.WithTags(Tags[$"{Paths.Products}/{Paths.Designer}"]));
		});
	}
}
