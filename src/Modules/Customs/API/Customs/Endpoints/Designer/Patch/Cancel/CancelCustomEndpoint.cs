using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Patch.Cancel;

public sealed class CancelCustomEndpoint(IRequestSender sender)
	: Endpoint<CancelCustomRequest>
{
	public override void Configure()
	{
		Patch("cancel");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Cancel")
			.WithDescription("Set a Custom's Status back to Pending")
		);
	}

	public override async Task HandleAsync(CancelCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new CancelCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
