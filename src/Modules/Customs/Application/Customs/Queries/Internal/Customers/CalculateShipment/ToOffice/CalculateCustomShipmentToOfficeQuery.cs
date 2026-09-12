using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToOffice;

public sealed record CalculateCustomShipmentToOfficeQuery(
	CustomId Id,
	int OfficeId,
	int Count,
	CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;
