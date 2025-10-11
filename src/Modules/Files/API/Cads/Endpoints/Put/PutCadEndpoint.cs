using CustomCADs.Files.Application.Cads.Commands.Internal.Edit;

namespace CustomCADs.Files.API.Cads.Endpoints.Put;

public sealed class PutCadEndpoint(IRequestSender sender) : Endpoint<PutCadRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CadsGroup>();
		Description(x => x
			.WithSummary("Change")
			.WithDescription("Change your Cad file")
		);
	}

	public override async Task HandleAsync(PutCadRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditCadCommand(
				Id: CadId.New(req.Id),
				ContentType: req.ContentType,
				Volume: req.Volume,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
