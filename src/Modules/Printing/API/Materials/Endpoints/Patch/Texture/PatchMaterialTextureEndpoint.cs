using CustomCADs.Printing.Application.Materials.Commands.Internal.SetTexture;

namespace CustomCADs.Printing.API.Materials.Endpoints.Patch.Texture;

public sealed class PatchMaterialTextureEndpoint(IRequestSender sender)
	: Endpoint<PatchMaterialTextureRequest>
{
	public override void Configure()
	{
		Patch("texture");
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("Change Texture")
			.WithDescription("Change your Material's Texture")
		);
	}

	public override async Task HandleAsync(PatchMaterialTextureRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new SetMaterialTextureCommand(
				Id: MaterialId.New(req.Id),
				ContentType: req.ContentType
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
