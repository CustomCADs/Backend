namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public sealed record DesignerGetAllProductsDto(
	ProductId Id,
	string Name,
	string CreatorName,
	DateTimeOffset UploadedAt,
	CategoryDto Category
);
