using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public sealed record GetAllShipmentsQuery(
	Pagination Pagination,
	AccountId? CallerId = null,
	Sorting<ShipmentSortingType>? Sorting = null
) : IQuery<Result<GetAllShipmentsDto>>;
