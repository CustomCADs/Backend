namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designers;

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
