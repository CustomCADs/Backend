using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;

public sealed class FinishCustomEndpoint(IRequestSender sender)
	: Endpoint<FinishCustomRequest>
{
	public override void Configure()
	{
		Patch("finish");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Finish")
			.WithDescription("Set an Custom's Status to Finished")
		);
	}

	public override async Task HandleAsync(FinishCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new FinishCustomCommand(
				Id: CustomId.New(req.Id),
				Price: req.Price,
				Cad: req.ToTuple(),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
