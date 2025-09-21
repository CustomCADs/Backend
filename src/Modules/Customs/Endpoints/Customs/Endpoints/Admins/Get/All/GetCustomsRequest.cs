using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Admins.Get.All;

public sealed record GetCustomsRequest(
	bool? ForDelivery = null,
	string? Name = null,
	CustomStatus? Status = null,
	Guid? CustomerId = null,
	Guid? DesignerId = null,
	CustomSortingType SortingType = CustomSortingType.OrderedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 50
);
