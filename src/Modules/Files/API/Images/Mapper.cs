using CustomCADs.Modules.Files.Application.Images.Dtos;

namespace CustomCADs.Modules.Files.API.Images;

internal static class Mapper
{
	extension(ImageDto image)
	{
		internal ImageResponse ToResponse()
			=> new(
				Id: image.Id.Value,
				Key: image.Key,
				ContentType: image.ContentType,
				OwnerName: image.OwnerName
			);
	}

}
