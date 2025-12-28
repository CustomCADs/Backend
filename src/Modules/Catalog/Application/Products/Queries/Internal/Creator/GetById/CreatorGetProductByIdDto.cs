using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public sealed record CreatorGetProductByIdDto(
	ProductId Id,
	string Name,
	string Description,
	decimal Price,
	string Status,
	DateTimeOffset UploadedAt,
	CountsDto Counts,
	string CreatorName,
	CategoryDto Category,
	CadId CadId,
	ImageId ImageId
);

