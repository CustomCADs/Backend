namespace CustomCADs.Modules.Files.API.Images.Endpoints.Post;

public record CreateImageRequest(
	string GeneratedKey,
	string ContentType
);
