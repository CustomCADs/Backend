namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class PurchasedCartsGroup : Group
{
	public PurchasedCartsGroup()
	{
		Configure(Paths.PurchasedCarts, x =>
		{
			x.Roles(CustomerRole);
			x.Description(x => x.WithTags(Tags[Paths.PurchasedCarts]));
		});
	}
}
