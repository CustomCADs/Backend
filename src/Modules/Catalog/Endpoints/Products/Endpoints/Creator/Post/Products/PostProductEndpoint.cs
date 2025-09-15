using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;

public sealed class PostProductEndpoint(IRequestSender sender)
	: Endpoint<PostProductRequest, PostProductResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create a Product")
		);
	}

	public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
	{
		ProductId id = await sender.SendCommandAsync(
			command: new CreateProductCommand(
				Name: req.Name,
				Description: req.Description,
				CategoryId: CategoryId.New(req.CategoryId),
				Price: req.Price,
				ImageKey: req.ImageKey,
				ImageContentType: req.ImageContentType,
				CadKey: req.CadKey,
				CadContentType: req.CadContentType,
				CadVolume: req.CadVolume,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		CreatorGetProductByIdDto dto = await sender.SendQueryAsync(
			query: new CreatorGetProductByIdQuery(
				Id: id,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		PostProductResponse response = dto.ToPostResponse();
		await Send.CreatedAtAsync<GetProductEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
	}
}
