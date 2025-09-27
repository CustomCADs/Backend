using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Dtos;

public record CompletedCustomResponse(
	PaymentStatus PaymentStatus,
	Guid? CustomizationId,
	Guid? ShipmentId
);
