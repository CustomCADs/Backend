using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Queries;

public sealed record CalculateShipmentToAddressQuery(
	double TotalWeight,
	string Country,
	string Site
) : IQuery<CalculateShipmentDto[]>;

public sealed record CalculateShipmentToOfficeQuery(
	double TotalWeight,
	int OfficeId
) : IQuery<CalculateShipmentDto[]>;
