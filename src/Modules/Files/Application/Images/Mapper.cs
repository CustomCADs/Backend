namespace CustomCADs.Modules.Files.Application.Images;

internal static class Mapper
{
	extension(Image image)
	{
		internal ImageDto ToDto(string ownerName)
			=> new(
				Id: image.Id,
				Key: image.Key,
				ContentType: image.ContentType,
				OwnerId: image.OwnerId,
				OwnerName: ownerName
			);
	}

}
