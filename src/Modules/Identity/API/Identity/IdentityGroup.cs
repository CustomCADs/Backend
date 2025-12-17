namespace CustomCADs.Modules.Identity.API.Identity;

using static APIConstants;

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
