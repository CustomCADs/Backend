using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Delivery.API.Shipments.Endpoints.Get.Shipment;

public class GetShipmentsEndpoint(IRequestSender sender)
	: Endpoint<GetShipmentsRequest, Result<GetShipmentsResponse>, GetShipmentsMapper>
{
	public override void Configure()
	{
		Get("");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Shipments with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetShipmentsRequest req, CancellationToken ct)
	{
		Result<GetAllShipmentsDto> result = await sender.SendQueryAsync(
			query: new GetAllShipmentsQuery(
				CallerId: User.GetAccountId(),
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
