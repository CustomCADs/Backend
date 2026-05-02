using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.Single;

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
		string? viewed = HttpContext.Request.Headers["X-Viewed-Product"];

		GalleryGetProductByIdDto product = await sender.SendQueryAsync(
			query: new GalleryGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CallerId: User.AccountId,
				Viewed: string.Equals(viewed, "true", StringComparison.CurrentCultureIgnoreCase)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(product, Map.FromEntity).ConfigureAwait(false);
	}
}
