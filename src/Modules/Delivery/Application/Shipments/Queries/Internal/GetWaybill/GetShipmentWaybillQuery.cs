using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

public sealed record GetShipmentWaybillQuery(
	ShipmentId Id,
	AccountId CallerId
) : IQuery<byte[]>;
