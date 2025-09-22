using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.API.Customs.Dtos;

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
