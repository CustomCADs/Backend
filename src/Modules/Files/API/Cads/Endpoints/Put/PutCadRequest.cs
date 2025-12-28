namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Put;

public sealed record PutCadRequest(
	Guid Id,
	string ContentType,
	decimal Volume
);
