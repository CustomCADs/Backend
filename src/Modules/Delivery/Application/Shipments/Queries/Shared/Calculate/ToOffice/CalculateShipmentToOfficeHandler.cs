using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Shared.Calculate.ToOffice;

public sealed class CalculateShipmentToOfficeHandler(IDeliveryService delivery)
	: IQueryHandler<CalculateShipmentToOfficeQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateShipmentToOfficeQuery req, CancellationToken ct)
	{
		CalculationDto[] calculations = await delivery.CalculateAsync(
			req: new(
				Weights: req.Weights,
				OfficeId: req.OfficeId,
				Destination: Destination.Office,
				Address: null
			),
			ct: ct
		).ConfigureAwait(false);

		return [.. calculations.Select(x => x.ToDto())];
	}
}
