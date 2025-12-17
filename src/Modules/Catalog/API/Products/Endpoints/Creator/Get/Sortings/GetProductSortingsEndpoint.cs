using CustomCADs.Modules.Catalog.Application.Products.Enums;
using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ProductCreatorSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Product Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ProductCreatorSortingType[] result = await sender.SendQueryAsync(
			query: new GetProductCreatorSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
