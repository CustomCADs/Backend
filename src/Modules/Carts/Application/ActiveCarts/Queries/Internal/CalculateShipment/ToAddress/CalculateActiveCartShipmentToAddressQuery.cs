using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToAddress;

public sealed record CalculateActiveCartShipmentToAddressQuery(
	AccountId CallerId,
	int CountryId,
	long SiteId
) : IQuery<CalculateShipmentDto[]>;
