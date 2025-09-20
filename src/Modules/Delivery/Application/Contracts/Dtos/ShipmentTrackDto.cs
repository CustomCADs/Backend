namespace CustomCADs.Delivery.Application.Contracts.Dtos;

public record ShipmentTrackDto(
	DateTimeOffset DateTime,
	bool IsDelivered,
	string? Place,
	string Message
);
