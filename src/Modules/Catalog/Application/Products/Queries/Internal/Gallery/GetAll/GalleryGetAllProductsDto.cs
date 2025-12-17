using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

public sealed record GalleryGetAllProductsDto(
	ProductId Id,
	string Name,
	string CreatorName,
	CountsDto Counts,
	string[] Tags,
	DateTimeOffset UploadedAt,
	CategoryDto Category,
	ImageId ImageId
);
