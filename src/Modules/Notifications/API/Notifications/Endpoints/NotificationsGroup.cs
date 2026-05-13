namespace CustomCADs.Modules.Notifications.API.Notifications.Endpoints;

using static APIConstants;
using static DomainConstants.Users;

public class NotificationsGroup : Group
{
	public NotificationsGroup()
	{
		Configure(Paths.Notifications, x =>
		{
			x.Roles(CustomerRole, ContributorRole, DesignerRole, AdminRole);
			x.Description(x => x.WithTags(Tags[Paths.Notifications]));
		});
	}
}
