using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Unchecked;

public sealed class GetUncheckedProductsEndpoint(IRequestSender sender)
	: Endpoint<GetUncheckedProductsRequest, Result<GetUncheckedProductsResponse>>
{
	public override void Configure()
	{
		Get("unchecked");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("All Unchecked")
			.WithDescription("See all Unchecked Products with Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetUncheckedProductsRequest req, CancellationToken ct)
	{
		Result<DesignerGetAllProductsDto> result = await sender.SendQueryAsync(
			query: new DesignerGetAllProductsQuery(
				CallerId: User.GetAccountId(),
				Status: ProductStatus.Unchecked,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToGetUncheckedDto())
		).ConfigureAwait(false);
	}
}
