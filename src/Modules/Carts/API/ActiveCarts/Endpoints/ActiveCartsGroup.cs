namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class ActiveCartsGroup : Group
{
	public ActiveCartsGroup()
	{
		Configure(Paths.ActiveCarts, x =>
		{
			x.Roles(CustomerRole);
			x.Description(x => x.WithTags(Tags[Paths.ActiveCarts]));
		});
	}
}
