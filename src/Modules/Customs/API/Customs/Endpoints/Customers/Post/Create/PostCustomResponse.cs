using CustomCADs.Modules.Customs.API.Customs.Dtos;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomResponse(
	Guid Id,
	string Name,
	string Description,
	bool ForDelivery,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	CustomCategoryResponse? Category
);
