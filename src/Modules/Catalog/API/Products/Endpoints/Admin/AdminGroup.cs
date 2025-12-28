namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Admin;

using static APIConstants;
using static DomainConstants.Roles;

public class AdminGroup : SubGroup<ProductsGroup>
{
	public AdminGroup()
	{
		Configure(Paths.Admin, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[$"{Paths.Products}/{Paths.Admin}"]));
		});
	}
}
