using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.All;

public class GetAllGalleryProductsMapper : ResponseMapper<GetAllGalleryProductsResponse, GalleryGetAllProductsDto>
{
	public override GetAllGalleryProductsResponse FromEntity(GalleryGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Tags: product.Tags,
			Category: product.Category.Name,
			Counts: product.Counts,
			ImageId: product.ImageId.Value
		);
}
