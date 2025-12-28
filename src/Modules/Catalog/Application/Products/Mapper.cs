using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetById;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Modules.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Modules.Catalog.Application.Products;

internal static class Mapper
{
	extension(Product product)
	{
		internal GalleryGetAllProductsDto ToGalleryGetAllDto(string username, string categoryName, string[] tags)
			=> new(
				Id: product.Id,
				Name: product.Name,
				Counts: product.Counts.ToDto(),
				Tags: tags,
				UploadedAt: product.UploadedAt,
				Category: new(product.CategoryId, categoryName),
				CreatorName: username,
				ImageId: product.ImageId
			);

		internal GalleryGetProductByIdDto ToGalleryGetByIdDto(string username, string categoryName, string[] tags)
			=> new(
				Id: product.Id,
				Name: product.Name,
				Description: product.Description,
				Price: product.Price,
				CreatorName: username,
				Tags: tags,
				UploadedAt: product.UploadedAt,
				Counts: product.Counts.ToDto(),
				Category: new(product.CategoryId, categoryName),
				CadId: product.CadId,
				ImageId: product.ImageId
			);

		internal CreatorGetAllProductsDto ToCreatorGetAllDto(string categoryName)
			=> new(
				Id: product.Id,
				Name: product.Name,
				Status: product.Status.ToString(),
				Views: product.Counts.Views,
				UploadedAt: product.UploadedAt,
				Category: new(product.CategoryId, categoryName),
				ImageId: product.ImageId
			);

		internal CreatorGetProductByIdDto ToCreatorGetByIdDto(string username, string categoryName)
			=> new(
				Id: product.Id,
				Name: product.Name,
				Description: product.Description,
				Price: product.Price,
				UploadedAt: product.UploadedAt,
				Status: product.Status.ToString(),
				Counts: product.Counts.ToDto(),
				Category: new(product.CategoryId, categoryName),
				CreatorName: username,
				CadId: product.CadId,
				ImageId: product.ImageId
			);

		internal DesignerGetAllProductsDto ToDesignerGetAllDto(string username, string categoryName)
			=> new(
				Id: product.Id,
				Name: product.Name,
				UploadedAt: product.UploadedAt,
				Category: new(product.CategoryId, categoryName),
				CreatorName: username
			);

		internal DesignerGetProductByIdDto ToDesignerGetByIdDto(string username, string categoryName)
			=> new(
				Id: product.Id,
				Name: product.Name,
				Description: product.Description,
				Price: product.Price,
				Category: new(product.CategoryId, categoryName),
				CreatorName: username
			);
	}

	extension(Counts counts)
	{
		internal CountsDto ToDto()
			=> new(
				Purchases: counts.Purchases,
				Views: counts.Views
			);
	}

}
