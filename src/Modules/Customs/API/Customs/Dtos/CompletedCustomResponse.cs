using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Dtos;

public record CompletedCustomResponse(
	PaymentStatus PaymentStatus,
	Guid? CustomizationId,
	Guid? ShipmentId
);
