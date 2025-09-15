using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

public sealed record CalculateActiveCartShipmentQuery(
	AccountId CallerId,
	AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
