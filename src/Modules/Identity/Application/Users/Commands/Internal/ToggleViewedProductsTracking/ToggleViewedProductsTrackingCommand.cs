namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

public sealed record ToggleViewedProductsTrackingCommand(
	string Username
) : ICommand;
