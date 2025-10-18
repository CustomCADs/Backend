namespace CustomCADs.Customs.API.Customs.Endpoints.Designer;

using static APIConstants;
using static DomainConstants.Roles;

public class DesignerGroup : SubGroup<CustomsGroup>
{
	public DesignerGroup()
	{
		Configure(Paths.Designer, x =>
		{
			x.Roles(Designer);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Designer}"]));
		});
	}
}
