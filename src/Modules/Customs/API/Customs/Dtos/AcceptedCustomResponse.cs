namespace CustomCADs.Customs.API.Customs.Dtos;

public record AcceptedCustomResponse(
	DateTimeOffset AcceptedAt,
	string DesignerName
);
