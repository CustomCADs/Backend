namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Admins;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class AdminGroup : Group
{
	public AdminGroup()
	{
		Configure(Paths.CustomsAdmin, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[Paths.CustomsAdmin]));
		});
	}
}
