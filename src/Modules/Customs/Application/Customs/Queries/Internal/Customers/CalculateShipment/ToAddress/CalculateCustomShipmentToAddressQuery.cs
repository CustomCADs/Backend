using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToAddress;

public sealed record CalculateCustomShipmentToAddressQuery(
	CustomId Id,
	int CountryId,
	long SiteId,
	int Count,
	CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;
