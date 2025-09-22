using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductDesignerSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Product Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ProductDesignerSortingType[] result = await sender.SendQueryAsync(
			query: new GetProductDesignerSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
