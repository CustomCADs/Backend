namespace CustomCADs.Catalog.API.Categories.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

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
