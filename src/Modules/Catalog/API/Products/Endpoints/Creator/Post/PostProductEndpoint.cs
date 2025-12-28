using CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Single;
using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Post;

public sealed class PostProductEndpoint(IRequestSender sender)
	: Endpoint<PostProductRequest, PostProductResponse, PostProductMapper>
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
				Price: req.Price,
				CategoryId: CategoryId.New(req.CategoryId),
				ImageId: ImageId.New(req.ImageId),
				CadId: CadId.New(req.CadId),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		CreatorGetProductByIdDto response = await sender.SendQueryAsync(
			query: new CreatorGetProductByIdQuery(
				Id: id,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.CreatedAtAsync<CreatorSingleProductEndpoint>(
			routeValues: new { Id = id.Value },
			responseBody: Map.FromEntity(response)
		).ConfigureAwait(false);
	}
}
