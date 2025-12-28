namespace CustomCADs.Modules.Files.API.Images.Endpoints.Put;

public sealed record PutImageRequest(
	Guid Id,
	string ContentType
);
