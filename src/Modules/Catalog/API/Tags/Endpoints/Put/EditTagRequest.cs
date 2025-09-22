namespace CustomCADs.Catalog.API.Tags.Endpoints.Put;

public record EditTagRequest(
	Guid Id,
	string Name
);
