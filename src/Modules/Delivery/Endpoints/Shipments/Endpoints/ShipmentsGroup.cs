namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

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
