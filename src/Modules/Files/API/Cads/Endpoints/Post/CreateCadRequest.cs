namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Post;

public record CreateCadRequest(
	string GeneratedKey,
	string ContentType,
	decimal Volume
);
