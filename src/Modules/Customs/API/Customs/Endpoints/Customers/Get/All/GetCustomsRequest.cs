using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.All;

public sealed record GetCustomsRequest(
	bool? ForDelivery = null,
	CustomStatus? Status = null,
	int? CategoryId = null,
	string? Name = null,
	CustomSortingType SortingType = CustomSortingType.OrderedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
