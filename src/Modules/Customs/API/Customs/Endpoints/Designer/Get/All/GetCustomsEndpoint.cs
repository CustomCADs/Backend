using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Get.All;

public sealed class GetCustomsEndpoint(IRequestSender sender)
	: Endpoint<GetCustomsRequest, Result<GetCustomsResponse>, GetCustomsMapper>
{
	public override void Configure()
	{
		Get("");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Customs with Filter, Search, Sort and Options options")
		);
	}

	public override async Task HandleAsync(GetCustomsRequest req, CancellationToken ct)
	{
		Result<GetAllCustomsDto> result = await sender.SendQueryAsync(
			query: new GetAllCustomsQuery(
				CustomStatus: CustomStatus.Finished,
				ForDelivery: req.ForDelivery,
				DesignerId: User.AccountId,
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
