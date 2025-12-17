namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned;

using static APIConstants;

public class PresignedGroup : SubGroup<CadsGroup>
{
	public PresignedGroup()
	{
		Configure(Paths.Presigned, x => { });
	}
}
