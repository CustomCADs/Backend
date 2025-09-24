using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Reported;

public class GetReportedProductsMapper : ResponseMapper<GetReportedProductsResponse, DesignerGetAllProductsDto>
{
	public override GetReportedProductsResponse FromEntity(DesignerGetAllProductsDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			UploadedAt: product.UploadedAt,
			CreatorName: product.CreatorName,
			Category: product.Category.ToResponse()
		);
}
