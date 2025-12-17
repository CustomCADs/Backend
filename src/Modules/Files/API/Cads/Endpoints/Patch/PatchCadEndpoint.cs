using CustomCADs.Modules.Files.Application.Cads.Commands.Internal.SetCoords;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Patch;

public sealed class PatchCadEndpoint(IRequestSender sender) : Endpoint<PatchCadRequest>
{
	public override void Configure()
	{
		Patch("");
		Group<CadsGroup>();
		Description(x => x
			.WithSummary("Coordinates")
			.WithDescription("Set the Coordinates of your Cad")
		);
	}

	public override async Task HandleAsync(PatchCadRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new SetCadCoordsCommand(
				Id: CadId.New(req.Id),
				CamCoordinates: req.CamCoordinates,
				PanCoordinates: req.PanCoordinates,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
