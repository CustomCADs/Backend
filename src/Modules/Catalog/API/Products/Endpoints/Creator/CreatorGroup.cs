namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator;

using static APIConstants;
using static DomainConstants.Users;

public class CreatorGroup : SubGroup<ProductsGroup>
{
	public CreatorGroup()
	{
		Configure(Paths.Creator, x =>
		{
			x.Roles(ContributorRole, DesignerRole);
			x.Description(x => x.WithTags(Tags[$"{Paths.Products}/{Paths.Creator}"]));
		});
	}
}
