using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Validated;

public sealed class GetValidatedProductsEndpoint(IRequestSender sender)
	: Endpoint<GetValidatedProductsRequest, Result<GetValidatedProductsResponse>>
{
	public override void Configure()
	{
		Get("validated");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("All Validated")
			.WithDescription("See all Validated Products with Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetValidatedProductsRequest req, CancellationToken ct)
	{
		Result<DesignerGetAllProductsDto> result = await sender.SendQueryAsync(
			query: new DesignerGetAllProductsQuery(
				CallerId: User.GetAccountId(),
				Status: ProductStatus.Validated,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToGetValidatedDto())
		).ConfigureAwait(false);
	}
}
