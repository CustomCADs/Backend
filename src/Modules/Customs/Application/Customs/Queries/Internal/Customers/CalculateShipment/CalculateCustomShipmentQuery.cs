using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

public sealed record CalculateCustomShipmentQuery(
	CustomId Id,
	int Count,
	AddressDto Address,
	CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;
