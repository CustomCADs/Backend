namespace CustomCADs.Catalog.API.Products.Endpoints.Creator;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class CreatorGroup : Group
{
	public CreatorGroup()
	{
		Configure(Paths.ProductsCreator, x =>
		{
			x.Roles(Contributor, Designer);
			x.Description(x => x.WithTags(Tags[Paths.ProductsCreator]));
		});
	}
}
