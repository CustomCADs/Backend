using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Designer.GetById;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Get.Single;

public sealed class DesignerSingleProductEndpoint(IRequestSender sender)
	: Endpoint<DesignerSingleProductRequest, DesignerSingleProductResponse, DesignerSingleProductMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Single Unchecked")
			.WithDescription("See an Unchecked Product")
		);
	}

	public override async Task HandleAsync(DesignerSingleProductRequest req, CancellationToken ct)
	{
		DesignerGetProductByIdDto product = await sender.SendQueryAsync(
			query: new DesignerGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(product, Map.FromEntity).ConfigureAwait(false);
	}
}
