namespace CustomCADs.Catalog.API.Categories.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class CategoriesGroup : Group
{
	public CategoriesGroup()
	{
		Configure(Paths.Categories, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[Paths.Categories]));
		});
	}
}
