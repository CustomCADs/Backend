using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public sealed record GetAllCustomsQuery(
	Pagination Pagination,
	CustomStatus? CustomStatus = null,
	AccountId? CustomerId = null,
	AccountId? DesignerId = null,
	bool? ForDelivery = null,
	string? Name = null,
	Sorting<CustomSortingType>? Sorting = null
) : IQuery<Result<GetAllCustomsDto>>;
