using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Single;

public class CreatorSingleProductMapper : ResponseMapper<CreatorSingleProductResponse, CreatorGetProductByIdDto>
{
	public override CreatorSingleProductResponse FromEntity(CreatorGetProductByIdDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Price: product.Price,
			Description: product.Description,
			UploadedAt: product.UploadedAt,
			Counts: product.Counts,
			Category: product.Category.ToResponse()
		);
}
