using CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Get.All;

public sealed class GetAllGalleryProductsEndpoint(IRequestSender sender)
	: Endpoint<GetAllGalleryProductsRequest, Result<GetAllGalleryProductsResponse>, GetAllGalleryProductsMapper>
{
	public override void Configure()
	{
		Get("");
		Group<GalleryGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all the Validated Products with Filter, Search, Sort and Pagination options")
		);
	}

	public override async Task HandleAsync(GetAllGalleryProductsRequest req, CancellationToken ct)
	{
		Result<GalleryGetAllProductsDto> result = await sender.SendQueryAsync(
			query: new GalleryGetAllProductsQuery(
				CallerId: User.AccountId,
				CategoryId: CategoryId.New(req.CategoryId),
				TagIds: TagId.New(req.TagIds),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
