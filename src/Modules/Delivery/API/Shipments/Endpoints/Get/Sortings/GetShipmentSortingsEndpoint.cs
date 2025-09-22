using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;
using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.API.Shipments.Endpoints.Get.Sortings;

public sealed class GetShipmentSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<ShipmentSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Shipment Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		ShipmentSortingType[] result = await sender.SendQueryAsync(
			query: new GetShipmentSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
