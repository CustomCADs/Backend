using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

namespace CustomCADs.Modules.Identity.API.Identity.Patch.ToggleViewedProductsTracking;

public sealed class ToggleViewedProductsTrackingEndpoint(IRequestSender sender)
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Patch("viewed-products");
		Group<IdentityGroup>();
		Description(x => x
			.WithName(IdentityNames.ToggleViewedProductsTracking)
			.WithSummary("Viewed Products Tracking")
			.WithDescription("Toggle whether the Products you View get Tracked")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ToggleViewedProductsTrackingCommand(
				Username: User.Name
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
