using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Get.Waybill;

using static DomainConstants.Roles;

public class GetShipmentWaybillEndpoint(IRequestSender sender)
	: Endpoint<GetShipmentWaybillRequest>
{
	public override void Configure()
	{
		Get("{id}/waybill");
		Group<ShipmentsGroup>();
		Roles(Designer);
		Description(x => x
			.WithSummary("Waybill")
			.WithDescription("Download this Shipment's waybill")
		);
	}

	public override async Task HandleAsync(GetShipmentWaybillRequest req, CancellationToken ct)
	{
		byte[] bytes = await sender.SendQueryAsync(
			query: new GetShipmentWaybillQuery(
				Id: ShipmentId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.BytesAsync(bytes, "waybill.pdf", "application/pdf").ConfigureAwait(false);
	}
}
