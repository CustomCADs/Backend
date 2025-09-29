namespace CustomCADs.Customs.API.Customs.Endpoints.Admins;

using static DomainConstants.Roles;
using static APIConstants;

public class AdminGroup : SubGroup<CustomsGroup>
{
	public AdminGroup()
	{
		Configure(Paths.Admin, x =>
		{
			x.Roles(Admin);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Admin}"]));
		});
	}
}
