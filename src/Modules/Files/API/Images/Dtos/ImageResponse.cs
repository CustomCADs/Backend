namespace CustomCADs.Modules.Files.API.Images.Dtos;

public record ImageResponse(
	Guid Id,
	string Key,
	string ContentType,
	string OwnerName
);
