using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.ValueObjects;

namespace CustomCADs.Customs.Domain.Repositories.Reads;

public record CustomQuery(
	Pagination Pagination,
	bool? ForDelivery = null,
	CustomStatus? CustomStatus = null,
	ProductId? ProductId = null,
	AccountId? CustomerId = null,
	AccountId? DesignerId = null,
	CategoryId? CategoryId = null,
	string? Name = null,
	Sorting<CustomSortingType>? Sorting = null
);
