namespace CustomCADs.Files.API.Cads.Endpoints.Post;

public record CreateCadRequest(
	string GeneratedKey,
	string ContentType,
	decimal Volume
);
