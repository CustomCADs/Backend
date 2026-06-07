namespace CustomCADs.Modules.Files.API.Images.Endpoints.Mutations.Post;

public record CreateImageRequest(
	string GeneratedKey,
	string ContentType
);
