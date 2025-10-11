namespace CustomCADs.Files.API.Cads.Endpoints.Post;

public record CreateCadRequest(
	string Key,
	string ContentType,
	decimal Volume
);
