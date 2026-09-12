using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToOffice;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Queries.Get.CalculateShipment.ToOffice;

public class CalculateActiveCartShipmentToOfficeEndpoint(IRequestSender sender)
	: Endpoint<CalculateActiveCartShipmentToOfficeRequest, ICollection<CalculateActiveCartShipmentResponse>, CalculateActiveCartShipmentMappper>
{
	public override void Configure()
	{
		Get("calculate/office");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment to Office")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment to your office")
		);
	}

	public override async Task HandleAsync(CalculateActiveCartShipmentToOfficeRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateActiveCartShipmentToOfficeQuery(
				CallerId: User.AccountId,
				OfficeId: req.OfficeId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(calculations, Map.FromEntity).ConfigureAwait(false);
	}
}
