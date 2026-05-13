namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Admins;

using static APIConstants;
using static DomainConstants.Users;

public class AdminGroup : SubGroup<CustomsGroup>
{
	public AdminGroup()
	{
		Configure(Paths.Admin, x =>
		{
			x.Roles(AdminRole);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Admin}"]));
		});
	}
}
