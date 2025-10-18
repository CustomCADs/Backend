namespace CustomCADs.Delivery.API.Shipments.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

public class ShipmentsGroup : Group
{
	public ShipmentsGroup()
	{
		Configure(Paths.Shipments, x =>
		{
			x.Roles(Customer);
			x.Description(x => x.WithTags(Tags[Paths.Shipments]));
		});
	}
}
