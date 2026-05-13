namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class CategoriesGroup : Group
{
	public CategoriesGroup()
	{
		Configure(Paths.Categories, x =>
		{
			x.Roles(AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Categories]));
		});
	}
}
