namespace CustomCADs.Files.Application.Images;

internal static class Mapper
{
	internal static ImageDto ToDto(this Image image, string ownerName)
		=> new(
			Id: image.Id,
			Key: image.Key,
			ContentType: image.ContentType,
			OwnerId: image.OwnerId,
			OwnerName: ownerName
		);
}
