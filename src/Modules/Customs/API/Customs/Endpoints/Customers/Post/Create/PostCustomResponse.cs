using CustomCADs.Customs.API.Customs.Dtos;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomResponse(
	Guid Id,
	string Name,
	string Description,
	bool ForDelivery,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	CustomCategoryResponse? Category
);
