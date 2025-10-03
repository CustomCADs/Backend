using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCad;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch.Cad;

public sealed class PatchProductCadEndpoint(IRequestSender sender)
	: Endpoint<PatchProductCadRequest>
{
	public override void Configure()
	{
		Patch("cad");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Change Cad")
			.WithDescription("Change your Product's Cad")
		);
	}

	public override async Task HandleAsync(PatchProductCadRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new SetProductCadCommand(
				Id: ProductId.New(req.Id),
				ContentType: req.ContentType,
				Volume: req.Volume,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
