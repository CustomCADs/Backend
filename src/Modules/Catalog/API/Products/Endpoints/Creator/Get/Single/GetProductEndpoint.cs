using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Get.Single;

public sealed class GetProductEndpoint(IRequestSender sender)
	: Endpoint<GetProductRequest, GetProductResponse>
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

	public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
	{
		CreatorGetProductByIdDto product = await sender.SendQueryAsync(
			query: new CreatorGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		GetProductResponse response = product.ToGetResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
