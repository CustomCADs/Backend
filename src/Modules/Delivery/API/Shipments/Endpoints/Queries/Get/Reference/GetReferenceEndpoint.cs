using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetReference;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Reference;

public class GetReferenceEndpoint(IRequestSender sender)
	: Endpoint<GetReferenceRequest, GetReferenceRequest, GetReferenceMapper>
{
	public override void Configure()
	{
		Get("{id}/reference");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Reference")
			.WithDescription("Obtain your Shipment's ReferenceId")
		);
	}

	public override async Task HandleAsync(GetReferenceRequest req, CancellationToken ct)
	{
		var dto = await sender.SendQueryAsync(
			query: new GetReferenceQuery(ShipmentId.New(req.Id), User.AccountId),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(dto, Map.FromEntity).ConfigureAwait(false);
	}
}
