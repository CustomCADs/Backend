using CustomCADs.Files.Application.Images.Dtos;

namespace CustomCADs.Files.API.Images;

internal static class Mapper
{
	internal static ImageResponse ToResponse(this ImageDto image)
		=> new(
			Id: image.Id.Value,
			Key: image.Key,
			ContentType: image.ContentType,
			OwnerName: image.OwnerName
		);
}
