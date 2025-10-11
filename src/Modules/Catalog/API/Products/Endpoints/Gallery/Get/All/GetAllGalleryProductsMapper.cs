using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.All;

public class GetAllGalleryProductsMapper : ResponseMapper<GetAllGalleryProductsResponse, GalleryGetAllProductsDto>
{
	public override GetAllGalleryProductsResponse FromEntity(GalleryGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Tags: product.Tags,
			Category: product.Category.Name,
			Views: product.Views,
			ImageId: product.ImageId.Value
		);
}
