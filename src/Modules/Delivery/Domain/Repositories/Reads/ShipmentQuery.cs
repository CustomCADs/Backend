using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Modules.Delivery.Domain.Repositories.Reads;

public record ShipmentQuery(
	Pagination Pagination,
	AccountId? CustomerId = null,
	Sorting<ShipmentSortingType>? Sorting = null
);
