namespace CustomCADs.Delivery.Application.Contracts.Dtos;

public record ShipmentStatusDto(
	DateTimeOffset DateTime,
	bool IsDelivered,
	string? Place,
	string Message
);
