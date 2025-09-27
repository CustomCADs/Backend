namespace CustomCADs.Shared.Application.UseCases.Shipments.Commands;

public record ActivateShipmentCommand(ShipmentId Id) : ICommand;
