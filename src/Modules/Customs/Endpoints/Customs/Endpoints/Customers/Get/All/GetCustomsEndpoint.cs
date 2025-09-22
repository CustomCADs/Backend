using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.All;

public sealed class GetCustomsEndpoint(IRequestSender sender)
	: Endpoint<GetCustomsRequest, Result<GetCustomsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Customs with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			query: new GetAllCustomsQuery(
				ForDelivery: req.ForDelivery,
				CustomStatus: req.Status,
				CustomerId: User.GetAccountId(),
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToCustomerResponse())
		).ConfigureAwait(false);
	}
}
