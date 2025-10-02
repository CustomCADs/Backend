namespace CustomCADs.Catalog.API.Products.Endpoints.Admin;

using static DomainConstants.Roles;
using static APIConstants;

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
