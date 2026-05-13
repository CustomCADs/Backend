namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class ShipmentsGroup : Group
{
	public ShipmentsGroup()
	{
		Configure(Paths.Shipments, x =>
		{
			x.Roles(CustomerRole);
			x.Description(x => x.WithTags(Tags[Paths.Shipments]));
		});
	}
}
