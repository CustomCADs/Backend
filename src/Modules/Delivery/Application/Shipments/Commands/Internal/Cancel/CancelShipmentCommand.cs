namespace CustomCADs.Modules.Delivery.Application.Shipments.Commands.Internal.Cancel;

public sealed record CancelShipmentCommand(
	ShipmentId Id,
	string Comment
) : ICommand;
