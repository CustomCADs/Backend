using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment.ToOffice;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Queries.Get.CalculateShipment.ToOffice;

public class CalculateCustomShipmentToOfficeEndpoint(IRequestSender sender)
	: Endpoint<CalculateCustomShipmentToOfficeRequest, ICollection<CalculateCustomShipmentResponse>, CalculateCustomShipmentMapper>
{
	public override void Configure()
	{
		Get("calculate/office/{id}");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Calculate Shipment")
			.WithDescription("Calculate the estimted price for the delivery of the Shipment to your office")
		);
	}

	public override async Task HandleAsync(CalculateCustomShipmentToOfficeRequest req, CancellationToken ct)
	{
		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			query: new CalculateCustomShipmentToOfficeQuery(
				Id: CustomId.New(req.Id),
				Count: req.Count,
				OfficeId: req.OfficeId,
				CustomizationId: CustomizationId.New(req.CustomizationId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(calculations, Map.FromEntity).ConfigureAwait(false);
	}
}
