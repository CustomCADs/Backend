namespace CustomCADs.Modules.Files.API.Images.Endpoints.Mutations.Put;

public sealed record PutImageRequest(
	Guid Id,
	string ContentType
);
