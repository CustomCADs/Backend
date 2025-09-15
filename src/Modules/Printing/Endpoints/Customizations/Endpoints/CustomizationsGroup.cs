namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints;

using static EndpointsConstants;

public class CustomizationsGroup : Group
{
	public CustomizationsGroup()
	{
		Configure(Paths.Customizations, x =>
		{
			x.AllowAnonymous();
			x.Description(x => x.WithTags(Tags[Paths.Customizations]));
		});
	}
}
