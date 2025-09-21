using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomResponse(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset OrderedAt,
	CustomStatus Status,
	bool ForDelivery
);
