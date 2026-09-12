using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Queries;

public sealed record CalculateShipmentToAddressQuery(
	double[] Weights,
	int CountryId,
	long SiteId
) : IQuery<CalculateShipmentDto[]>;

public sealed record CalculateShipmentToOfficeQuery(
	double[] Weights,
	int OfficeId
) : IQuery<CalculateShipmentDto[]>;
