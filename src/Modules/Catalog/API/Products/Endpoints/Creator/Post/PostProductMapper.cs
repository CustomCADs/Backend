using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Post;

public class PostProductMapper : ResponseMapper<PostProductResponse, CreatorGetProductByIdDto>
{
	public override PostProductResponse FromEntity(CreatorGetProductByIdDto product)
		=> new(
			Id: product.Id.Value,
			Name: product.Name,
			Description: product.Description,
			Price: product.Price,
			Status: product.Status,
			UploadedAt: product.UploadedAt,
			CreatorName: product.CreatorName,
			Category: product.Category.ToResponse(),
			CadId: product.CadId.Value,
			ImageId: product.ImageId.Value
		);
}
