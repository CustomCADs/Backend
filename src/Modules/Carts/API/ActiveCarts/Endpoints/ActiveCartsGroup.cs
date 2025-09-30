namespace CustomCADs.Carts.API.ActiveCarts.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

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
