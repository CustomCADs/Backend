namespace CustomCADs.Modules.Files.API.Images.Endpoints.Presigned;

using static APIConstants;

public class PresignedGroup : SubGroup<ImagesGroup>
{
	public PresignedGroup()
	{
		Configure(Paths.Presigned, x => { });
	}
}
