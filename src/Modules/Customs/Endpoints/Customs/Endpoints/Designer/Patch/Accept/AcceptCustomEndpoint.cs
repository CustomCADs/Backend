using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Accept;

public sealed class AcceptCustomEndpoint(IRequestSender sender)
	: Endpoint<AcceptCustomRequest>
{
	public override void Configure()
	{
		Patch("accept");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Accept")
			.WithDescription("Set an Custom's Status to Accepted")
		);
	}

	public override async Task HandleAsync(AcceptCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new AcceptCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
