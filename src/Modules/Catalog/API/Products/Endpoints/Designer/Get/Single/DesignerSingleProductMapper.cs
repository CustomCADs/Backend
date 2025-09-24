using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Single;

public class DesignerSingleProductMapper : ResponseMapper<DesignerSingleProductResponse, DesignerGetProductByIdDto>
{
	public override DesignerSingleProductResponse FromEntity(DesignerGetProductByIdDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Description: product.Description,
			Price: product.Price,
			CreatorName: product.CreatorName,
			Category: new(product.Category.Id.Value, product.Category.Name)
		);
}
