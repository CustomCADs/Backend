using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;

public sealed class GetShipmentHandler(
	IShipmentReads reads,
	IDeliveryService delivery
) : IQueryHandler<GetShipmentQuery, GetShipmentDto>
{
	public async Task<GetShipmentDto> Handle(GetShipmentQuery req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		if (req.CallerId != shipment.BuyerId)
			throw CustomAuthorizationException<Shipment>.ById(req.Id);

		var calculations = await delivery.CalculateAsync(
			req: new(
				shipment.Info.Weight,
				shipment.Delivery.Destination,
				shipment.Delivery.OfficeId,
				shipment.Delivery.Address?.ToTuple()
			),
			ct
		).ConfigureAwait(false);
		var calculation = calculations.First((x) => x.Service == shipment.Reference.Service);

		return new(
			Id: shipment.Id,
			RequestedAt: shipment.RequestedAt,
			Status: shipment.Status,
			Info: shipment.Info,
			Fee: calculation.Price.Amount
		);
	}
}
