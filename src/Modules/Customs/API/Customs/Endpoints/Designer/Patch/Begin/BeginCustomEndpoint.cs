using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Begin;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Patch.Begin;

public sealed class BeginCustomEndpoint(IRequestSender sender)
	: Endpoint<BeginCustomRequest>
{
	public override void Configure()
	{
		Patch("begin");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Begin")
			.WithDescription("Set a Custom's Status to Begun")
		);
	}

	public override async Task HandleAsync(BeginCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new BeginCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
