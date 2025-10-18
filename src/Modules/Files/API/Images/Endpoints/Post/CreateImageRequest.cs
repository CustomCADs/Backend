namespace CustomCADs.Files.API.Images.Endpoints.Post;

public record CreateImageRequest(
	string GeneratedKey,
	string ContentType
);
