namespace CustomCADs.Customs.API.Customs.Endpoints;

using static EndpointsConstants;

public class CustomsGroup : Group
{
	public CustomsGroup()
	{
		Configure(Paths.Customs, _ => { });
	}
}
