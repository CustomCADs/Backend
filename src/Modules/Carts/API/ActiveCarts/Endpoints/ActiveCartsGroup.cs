namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class ActiveCartsGroup : Group
{
	public ActiveCartsGroup()
	{
		Configure(Paths.ActiveCarts, x =>
		{
			x.Roles(Customer);
			x.Description(x => x.WithTags(Tags[Paths.ActiveCarts]));
		});
	}
}
