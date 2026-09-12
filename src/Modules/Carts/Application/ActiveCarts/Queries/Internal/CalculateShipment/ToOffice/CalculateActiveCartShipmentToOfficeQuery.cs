using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToOffice;

public sealed record CalculateActiveCartShipmentToOfficeQuery(
	AccountId CallerId,
	int OfficeId
) : IQuery<CalculateShipmentDto[]>;
