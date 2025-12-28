namespace CustomCADs.Modules.Customs.API.Customs.Dtos;

public record FinishedCustomResponse(
	decimal Price,
	DateTimeOffset FinishedAt,
	Guid CadId
);
