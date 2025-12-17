using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Domain.Products.Enums;

namespace CustomCADs.Modules.Catalog.API.Products;

internal static class Mapper
{
	extension(CategoryDto category)
	{
		internal CategoryDtoResponse ToResponse()
			=> new(
				Id: category.Id.Value,
				Name: category.Name
			);
	}

	extension(ProductGallerySortingType sorting)
	{
		internal ProductSortingType ToBase()
			=> sorting switch
			{
				ProductGallerySortingType.UploadedAt => ProductSortingType.UploadedAt,
				ProductGallerySortingType.Alphabetical => ProductSortingType.Alphabetical,
				ProductGallerySortingType.Cost => ProductSortingType.Cost,
				ProductGallerySortingType.Purchases => ProductSortingType.Purchases,
				ProductGallerySortingType.Views => ProductSortingType.Views,
				_ => throw new ArgumentException("", nameof(sorting))
			};
	}

	extension(ProductCreatorSortingType sorting)
	{
		internal ProductSortingType ToBase()
			=> sorting switch
			{
				ProductCreatorSortingType.UploadedAt => ProductSortingType.UploadedAt,
				ProductCreatorSortingType.Alphabetical => ProductSortingType.Alphabetical,
				ProductCreatorSortingType.Status => ProductSortingType.Status,
				ProductCreatorSortingType.Cost => ProductSortingType.Cost,
				ProductCreatorSortingType.Purchases => ProductSortingType.Purchases,
				ProductCreatorSortingType.Views => ProductSortingType.Views,
				_ => throw new ArgumentException("", nameof(sorting))
			};
	}

	extension(ProductDesignerSortingType sorting)
	{
		internal ProductSortingType ToBase()
			=> sorting switch
			{
				ProductDesignerSortingType.UploadedAt => ProductSortingType.UploadedAt,
				ProductDesignerSortingType.Alphabetical => ProductSortingType.Alphabetical,
				ProductDesignerSortingType.Cost => ProductSortingType.Cost,
				_ => throw new ArgumentException("", nameof(sorting))
			};
	}

}
