using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.Single;

public class GetGalleryProductMapper : ResponseMapper<GetGalleryProductResponse, GalleryGetProductByIdDto>
{
	public override GetGalleryProductResponse FromEntity(GalleryGetProductByIdDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Description: product.Description,
			Price: product.Price,
			Tags: product.Tags,
			CreatorName: product.CreatorName,
			UploadedAt: product.UploadedAt,
			Counts: product.Counts,
			Category: product.Category.ToResponse(),
			CadId: product.CadId.Value,
			ImageId: product.ImageId.Value
		);
}
