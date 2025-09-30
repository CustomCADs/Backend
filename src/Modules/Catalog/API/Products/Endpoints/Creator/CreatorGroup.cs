namespace CustomCADs.Catalog.API.Products.Endpoints.Creator;

using static DomainConstants.Roles;
using static APIConstants;

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
