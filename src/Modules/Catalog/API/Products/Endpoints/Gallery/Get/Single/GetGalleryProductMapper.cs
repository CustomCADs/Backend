using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.Single;

public class GetGalleryProductMapper : ResponseMapper<GetGalleryProductResponse, GalleryGetProductByIdDto>
{
	public override GetGalleryProductResponse FromEntity(GalleryGetProductByIdDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Description: product.Description,
			Price: product.Price,
			Volume: product.Volume,
			Tags: product.Tags,
			CreatorName: product.CreatorName,
			UploadedAt: product.UploadedAt,
			CamCoordinates: product.CamCoordinates,
			PanCoordinates: product.PanCoordinates,
			Counts: product.Counts,
			Category: product.Category.ToResponse()
		);
}
