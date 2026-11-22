using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;

public sealed record GetShipmentQuery(
	ShipmentId Id,
	AccountId CallerId
) : IQuery<GetShipmentDto>;
