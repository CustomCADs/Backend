using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

[AddRequestCaching(ExpirationType.Absolute, TimeType.Minute, 1)]
public sealed record GalleryGetProductByIdDto(
	ProductId Id,
	string Name,
	string Description,
	decimal Price,
	string CreatorName,
	string[] Tags,
	DateTimeOffset UploadedAt,
	CountsDto Counts,
	CategoryDto Category,
	CadId CadId,
	ImageId ImageId
);
