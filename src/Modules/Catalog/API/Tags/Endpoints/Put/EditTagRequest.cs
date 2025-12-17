namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Put;

public record EditTagRequest(
	Guid Id,
	string Name
);
