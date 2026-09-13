using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;

namespace CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

public record ShipRequestDto(
	DeliveryDestination Destination,
	int? OfficeId,
	ShipmentAddress? Address,
	string Phone,
	string Email,
	string Name,
	string Service,
	string Package,
	string Contents,
	double TotalWeight
);
