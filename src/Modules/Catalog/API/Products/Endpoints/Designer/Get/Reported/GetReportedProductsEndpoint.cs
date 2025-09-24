using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Get.Reported;

public sealed class GetReportedProductsEndpoint(IRequestSender sender)
	: Endpoint<GetReportedProductsRequest, Result<GetReportedProductsResponse>, GetReportedProductsMapper>
{
	public override void Configure()
	{
		Get("reported");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("All Reported")
			.WithDescription("See all Reported Products with Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetReportedProductsRequest req, CancellationToken ct)
	{
		Result<DesignerGetAllProductsDto> result = await sender.SendQueryAsync(
			query: new DesignerGetAllProductsQuery(
				CallerId: User.GetAccountId(),
				Status: ProductStatus.Reported,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
