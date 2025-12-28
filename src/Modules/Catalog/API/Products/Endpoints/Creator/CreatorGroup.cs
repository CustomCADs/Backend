namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator;

using static APIConstants;
using static DomainConstants.Roles;

public class CreatorGroup : SubGroup<ProductsGroup>
{
	public CreatorGroup()
	{
		Configure(Paths.Creator, x =>
		{
			x.Roles(Contributor, Designer);
			x.Description(x => x.WithTags(Tags[$"{Paths.Products}/{Paths.Creator}"]));
		});
	}
}
