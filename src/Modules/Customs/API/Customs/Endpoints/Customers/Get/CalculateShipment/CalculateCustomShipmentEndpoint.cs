using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.CalculateShipment;

public class CalculateCustomShipmentEndpoint(IRequestSender sender)
	: Endpoint<CalculateCustomShipmentRequest, ICollection<CalculateCustomShipmentResponse>>
{
	public override void Configure()
	{
		Get("calculate/{id}");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment")
		);
	}

	public override async Task HandleAsync(CalculateCustomShipmentRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateCustomShipmentQuery(
				Id: CustomId.New(req.Id),
				Count: req.Count,
				Address: new(req.Country, req.City, req.Street),
				CustomizationId: CustomizationId.New(req.CustomizationId)
			),
			ct: ct
		).ConfigureAwait(false);

		ICollection<CalculateCustomShipmentResponse> response =
			[.. calculations.Select(x => x.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
