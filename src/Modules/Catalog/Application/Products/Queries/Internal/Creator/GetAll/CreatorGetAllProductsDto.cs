using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

public sealed record CreatorGetAllProductsDto(
	ProductId Id,
	string Name,
	string Status,
	int Views,
	DateTimeOffset UploadedAt,
	CategoryDto Category,
	ImageId ImageId
);
