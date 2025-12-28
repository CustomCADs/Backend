namespace CustomCADs.Modules.Customs.API.Customs.Endpoints;

using static APIConstants;

public class CustomsGroup : Group
{
	public CustomsGroup()
	{
		Configure(Paths.Customs, _ => { });
	}
}
