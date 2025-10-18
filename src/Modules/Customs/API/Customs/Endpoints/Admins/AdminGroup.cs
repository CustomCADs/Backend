namespace CustomCADs.Customs.API.Customs.Endpoints.Admins;

using static APIConstants;
using static DomainConstants.Roles;

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
