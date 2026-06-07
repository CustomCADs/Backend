namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Mutations.Post;

public record CreateCadRequest(
	string GeneratedKey,
	string ContentType,
	decimal Volume
);
