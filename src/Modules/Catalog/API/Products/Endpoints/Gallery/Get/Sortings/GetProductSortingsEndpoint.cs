using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductGallerySortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<GalleryGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Product Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ProductGallerySortingType[] result = await sender.SendQueryAsync(
			query: new GetProductGallerySortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
