using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Get.Single;

public sealed class GetGalleryProductEndpoint(IRequestSender sender)
	: Endpoint<GetGalleryProductRequest, GetGalleryProductResponse, GetGalleryProductMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<GalleryGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See a Validated Product in detail")
		);
	}

	public override async Task HandleAsync(GetGalleryProductRequest req, CancellationToken ct)
	{
		GalleryGetProductByIdDto product = await sender.SendQueryAsync(
			query: new GalleryGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(product, Map.FromEntity).ConfigureAwait(false);
	}
}
