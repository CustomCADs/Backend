using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetReference;

public sealed record GetReferenceQuery(
	ShipmentId Id,
	AccountId CallerId
) : IQuery<GetReferenceDto>;
