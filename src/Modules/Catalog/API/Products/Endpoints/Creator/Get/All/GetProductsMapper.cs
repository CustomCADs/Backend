using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.All;

public class GetProductsMapper : ResponseMapper<GetProductsResponse, CreatorGetAllProductsDto>
{
	public override GetProductsResponse FromEntity(CreatorGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			UploadedAt: product.UploadedAt,
			Category: product.Category.ToResponse(),
			ImageId: product.ImageId.Value
		);
}
