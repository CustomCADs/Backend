namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class NotificationsGroup : Group
{
	public NotificationsGroup()
	{
		Configure(Paths.Notifications, ep =>
		{
			ep.Roles(Customer, Contributor, Designer, Admin);
			ep.Description(d => d.WithTags(Tags[Paths.Notifications]));
		});
	}
}
