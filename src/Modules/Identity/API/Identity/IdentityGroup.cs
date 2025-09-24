namespace CustomCADs.Identity.API.Identity;

using static EndpointsConstants;

public class IdentityGroup : Group
{
	public IdentityGroup()
	{
		Configure(Paths.Identity, x =>
		{
			x.Description(x => x.WithTags(Tags[Paths.Identity]));
		});
	}
}
