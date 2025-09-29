namespace CustomCADs.Carts.API.PurchasedCarts.Endpoints;

using static DomainConstants.Roles;
using static APIConstants;

public class PurchasedCartsGroup : Group
{
	public PurchasedCartsGroup()
	{
		Configure(Paths.PurchasedCarts, x =>
		{
			x.Roles(Customer);
			x.Description(x => x.WithTags(Tags[Paths.PurchasedCarts]));
		});
	}
}
