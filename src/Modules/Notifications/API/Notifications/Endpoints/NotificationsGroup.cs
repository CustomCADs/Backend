namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints;

using static APIConstants;
using static DomainConstants.Roles;

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
