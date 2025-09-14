namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class CreatorGroup : Group
{
	public CreatorGroup()
	{
		Configure(Paths.ProductsCreator, ep =>
		{
			ep.Roles(Contributor, Designer);
			ep.Description(d => d.WithTags(Tags[Paths.ProductsCreator]));
		});
	}
}
