using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetShipment;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Shipment;

public class GetShipmentEndpoint(IRequestSender sender)
	: Endpoint<GetShipmentRequest, Result<GetShipmentResponse>, GetShipmentMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Shipments details")
		);
	}

	public override async Task HandleAsync(GetShipmentRequest req, CancellationToken ct)
	{
		GetShipmentDto result = await sender.SendQueryAsync(
			query: new GetShipmentQuery(
				Id: ShipmentId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
