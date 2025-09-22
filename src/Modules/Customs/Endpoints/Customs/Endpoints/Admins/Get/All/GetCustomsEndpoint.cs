using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Admins.Get.All;

public sealed class GetCustomsEndpoint(IRequestSender sender)
	: Endpoint<GetCustomsRequest, Result<GetCustomsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<AdminGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Customs with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			query: new GetAllCustomsQuery(
				ForDelivery: req.ForDelivery,
				CustomStatus: req.Status,
				CustomerId: AccountId.New(req.CustomerId),
				DesignerId: AccountId.New(req.DesignerId),
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(
			response: result.ToNewResult(x => x.ToAdminResponse())
		).ConfigureAwait(false);
	}
}
