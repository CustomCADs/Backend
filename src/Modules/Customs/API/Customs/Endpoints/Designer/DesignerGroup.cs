namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer;

using static APIConstants;
using static DomainConstants.Users;

public class DesignerGroup : SubGroup<CustomsGroup>
{
	public DesignerGroup()
	{
		Configure(Paths.Designer, x =>
		{
			x.Roles(DesignerRole);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Designer}"]));
		});
	}
}
