using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.API.Products;

internal static class Mapper
{
	internal static CategoryDtoResponse ToResponse(this CategoryDto category)
		=> new(
			Id: category.Id.Value,
			Name: category.Name
		);

	internal static ProductSortingType ToBase(this ProductGallerySortingType sorting)
		=> sorting switch
		{
			ProductGallerySortingType.UploadedAt => ProductSortingType.UploadedAt,
			ProductGallerySortingType.Alphabetical => ProductSortingType.Alphabetical,
			ProductGallerySortingType.Cost => ProductSortingType.Cost,
			ProductGallerySortingType.Purchases => ProductSortingType.Purchases,
			ProductGallerySortingType.Views => ProductSortingType.Views,
			_ => throw new ArgumentException("", nameof(sorting))
		};

	internal static ProductSortingType ToBase(this ProductCreatorSortingType sorting)
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

	internal static ProductSortingType ToBase(this ProductDesignerSortingType sorting)
		=> sorting switch
		{
			ProductDesignerSortingType.UploadedAt => ProductSortingType.UploadedAt,
			ProductDesignerSortingType.Alphabetical => ProductSortingType.Alphabetical,
			ProductDesignerSortingType.Cost => ProductSortingType.Cost,
			_ => throw new ArgumentException("", nameof(sorting))
		};
}
