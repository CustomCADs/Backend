using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Shared.Calculate.ToAddress;

public sealed class CalculateShipmentToAddressHandler(IDeliveryService delivery)
	: IQueryHandler<CalculateShipmentToAddressQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateShipmentToAddressQuery req, CancellationToken ct)
	{
		CalculationDto[] calculations = await delivery.CalculateAsync(
			req: new(
				Weights: req.Weights,
				Address: new(req.CountryId, req.SiteId),
				Destination: Destination.Address,
				OfficeId: null
			),
			ct: ct
		).ConfigureAwait(false);

		return [.. calculations.Select(x => x.ToDto())];
	}
}
