using CustomCADs.Modules.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment.ToAddress;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Modules.Carts.API.ActiveCarts.Endpoints.Queries.Get.CalculateShipment.ToAddress;

public class CalculateActiveCartShipmentToAddressEndpoint(IRequestSender sender)
	: Endpoint<CalculateActiveCartShipmentToAddressRequest, ICollection<CalculateActiveCartShipmentResponse>, CalculateActiveCartShipmentMappper>
{
	public override void Configure()
	{
		Get("calculate/address");
		Group<ActiveCartsGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment to Address")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment to your address")
		);
	}

	public override async Task HandleAsync(CalculateActiveCartShipmentToAddressRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateActiveCartShipmentToAddressQuery(
				CallerId: User.AccountId,
				Country: req.Country,
				Site: req.Site
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(calculations, Map.FromEntity).ConfigureAwait(false);
	}
}
