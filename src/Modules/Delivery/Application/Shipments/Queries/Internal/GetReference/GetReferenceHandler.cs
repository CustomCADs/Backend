using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetReference;

public sealed class GetReferenceHandler(IShipmentReads reads) : IQueryHandler<GetReferenceQuery, GetReferenceDto>
{
	public async Task<GetReferenceDto> Handle(GetReferenceQuery req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		if (req.CallerId != shipment.BuyerId)
			throw CustomAuthorizationException<Shipment>.ById(req.Id);

		if (shipment is not { Reference.Id: not null, Status: ShipmentStatus.Active or ShipmentStatus.Delivered })
			throw CustomStatusException<Shipment>.ById(req.Id);

		return new(
			Id: shipment.Reference.Id,
			Service: shipment.Reference.Service
		);
	}
}
