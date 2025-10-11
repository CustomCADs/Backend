using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Customs.API.Customs.Endpoints.Designer.Patch.Finish;

public sealed class FinishCustomEndpoint(IRequestSender sender)
	: Endpoint<FinishCustomRequest>
{
	public override void Configure()
	{
		Patch("finish");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Finish")
			.WithDescription("Set a Custom's Status to Finished")
		);
	}

	public override async Task HandleAsync(FinishCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new FinishCustomCommand(
				Id: CustomId.New(req.Id),
				Price: req.Price,
				CadId: CadId.New(req.CadId),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
