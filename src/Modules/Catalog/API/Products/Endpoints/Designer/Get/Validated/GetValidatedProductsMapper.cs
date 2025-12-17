using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Get.Validated;

public class GetValidatedProductsMapper : ResponseMapper<GetValidatedProductsResponse, DesignerGetAllProductsDto>
{
	public override GetValidatedProductsResponse FromEntity(DesignerGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			UploadedAt: product.UploadedAt,
			CreatorName: product.CreatorName,
			Category: product.Category.ToResponse()
		);
}
