using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Admins.Get.All;

public sealed record GetCustomsRequest(
	bool? ForDelivery = null,
	string? Name = null,
	CustomStatus? Status = null,
	Guid? CustomerId = null,
	Guid? DesignerId = null,
	int? CategoryId = null,
	CustomSortingType SortingType = CustomSortingType.OrderedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 50
);
