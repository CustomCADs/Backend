using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Unchecked;

public class GetUncheckedProductsMapper : ResponseMapper<GetUncheckedProductsResponse, DesignerGetAllProductsDto>
{
	public override GetUncheckedProductsResponse FromEntity(DesignerGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			UploadedAt: product.UploadedAt,
			CreatorName: product.CreatorName,
			Category: product.Category.ToResponse()
		);
}
