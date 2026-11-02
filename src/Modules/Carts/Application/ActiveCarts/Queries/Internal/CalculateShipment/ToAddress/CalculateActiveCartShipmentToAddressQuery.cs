using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToAddress;

public sealed record CalculateActiveCartShipmentToAddressQuery(
	AccountId CallerId,
	string Country,
	string Site
) : IQuery<CalculateShipmentDto[]>;
