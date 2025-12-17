using CustomCADs.Modules.Delivery.Application.Shipments.Commands.Internal.Cancel;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Patch.Cancel;

public class CancelShipmentEndpoint(IRequestSender sender)
	: Endpoint<CancelShipmentRequest>
{
	public override void Configure()
	{
		Patch("");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Cancel")
			.WithDescription("Cancel a Shipment")
		);
	}

	public override async Task HandleAsync(CancelShipmentRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new CancelShipmentCommand(
				Id: ShipmentId.New(req.Id),
				Comment: req.Comment
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
