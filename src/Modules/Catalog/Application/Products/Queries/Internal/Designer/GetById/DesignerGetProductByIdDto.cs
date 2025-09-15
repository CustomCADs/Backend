namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

public sealed record DesignerGetProductByIdDto(
	ProductId Id,
	string Name,
	string Description,
	decimal Price,
	string CreatorName,
	CategoryDto Category
);
