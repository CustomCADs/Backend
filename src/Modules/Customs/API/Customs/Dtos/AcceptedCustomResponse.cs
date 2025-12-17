namespace CustomCADs.Modules.Customs.API.Customs.Dtos;

public record AcceptedCustomResponse(
	DateTimeOffset AcceptedAt,
	string DesignerName
);
