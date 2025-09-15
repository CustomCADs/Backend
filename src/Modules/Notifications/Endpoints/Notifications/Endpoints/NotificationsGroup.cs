namespace CustomCADs.Notifications.Endpoints.Notifications.Endpoints;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class NotificationsGroup : Group
{
	public NotificationsGroup()
	{
		Configure(Paths.Notifications, x =>
		{
			x.Roles(Customer, Contributor, Designer, Admin);
			x.Description(x => x.WithTags(Tags[Paths.Notifications]));
		});
	}
}
