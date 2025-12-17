using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Recent;

public class RecentProductsMapper : ResponseMapper<RecentProductsResponse, CreatorGetAllProductsDto>
{
	public override RecentProductsResponse FromEntity(CreatorGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Status: product.Status,
			UploadedAt: product.UploadedAt,
			Category: product.Category.ToResponse()
		);
}
