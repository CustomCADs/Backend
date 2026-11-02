using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToAddress;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.CalculateShipment.ToAddress;

public class CalculateCustomShipmentToAddressEndpoint(IRequestSender sender)
	: Endpoint<CalculateCustomShipmentToAddressRequest, ICollection<CalculateCustomShipmentResponse>, CalculateCustomShipmentMapper>
{
	public override void Configure()
	{
		Get("calculate/address/{id}");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment to your address")
		);
	}

	public override async Task HandleAsync(CalculateCustomShipmentToAddressRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateCustomShipmentToAddressQuery(
				Id: CustomId.New(req.Id),
				Count: req.Count,
				Country: req.Country,
				Site: req.Site,
				CustomizationId: CustomizationId.New(req.CustomizationId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(calculations, Map.FromEntity).ConfigureAwait(false);
	}
}
