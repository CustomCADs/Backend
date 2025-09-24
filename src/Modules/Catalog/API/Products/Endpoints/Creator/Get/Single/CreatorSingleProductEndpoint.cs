using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Single;

public sealed class CreatorSingleProductEndpoint(IRequestSender sender)
	: Endpoint<CreatorSingleProductRequest, CreatorSingleProductResponse, CreatorSingleProductMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Product in detail")
		);
	}

	public override async Task HandleAsync(CreatorSingleProductRequest req, CancellationToken ct)
	{
		CreatorGetProductByIdDto product = await sender.SendQueryAsync(
			query: new CreatorGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(product, Map.FromEntity).ConfigureAwait(false);
	}
}
