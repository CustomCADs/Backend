using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Endpoints.Customs.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomResponse(
	Guid Id,
	string Name,
	string Description,
	bool ForDelivery,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	CustomCategoryResponse? Category
);
