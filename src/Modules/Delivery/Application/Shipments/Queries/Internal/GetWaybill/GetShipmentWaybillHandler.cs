using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories.Reads;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

using static DomainConstants.Users;

public sealed class GetShipmentWaybillHandler(
	IShipmentReads reads,
	IDeliveryService delivery
) : IQueryHandler<GetShipmentWaybillQuery, byte[]>
{
	public async Task<byte[]> Handle(GetShipmentWaybillQuery req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		Guid headDesignerId = Guid.Parse(DesignerAccountId);
		if (req.CallerId.Value != headDesignerId)
		{
			throw CustomAuthorizationException<Shipment>.ById(req.Id);
		}

		return await delivery.PrintAsync(shipment.ReferenceId, ct: ct).ConfigureAwait(false);
	}
}
