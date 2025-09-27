using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Carts.API.ActiveCarts.Endpoints.Get.CalculateShipment;

public class CalculateActiveCartShipmentEndpoint(IRequestSender sender)
	: Endpoint<CalculateActiveCartShipmentRequest, ICollection<CalculateActiveCartShipmentResponse>, CalculateActiveCartShipmentMappper>
{
	public override void Configure()
	{
		Get("calculate");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment")
		);
	}

	public override async Task HandleAsync(CalculateActiveCartShipmentRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateActiveCartShipmentQuery(
				CallerId: User.GetAccountId(),
				Address: new(req.Country, req.City, req.Street)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(calculations, Map.FromEntity).ConfigureAwait(false);
	}
}
