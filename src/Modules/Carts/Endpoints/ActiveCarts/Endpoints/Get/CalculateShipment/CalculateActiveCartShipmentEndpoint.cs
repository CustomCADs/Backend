using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;

public class CalculateActiveCartShipmentEndpoint(IRequestSender sender)
	: Endpoint<CalculateActiveCartShipmentRequest, ICollection<CalculateActiveCartShipmentResponse>>
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

		ICollection<CalculateActiveCartShipmentResponse> response =
			[.. calculations.Select(c => c.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
