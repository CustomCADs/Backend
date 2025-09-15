using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Queries;

public sealed record CalculateShipmentQuery(
	double[] Weights,
	AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
