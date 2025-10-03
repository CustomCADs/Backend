using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetImage;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch.Image;

public sealed class PatchProductImageEndpoint(IRequestSender sender)
	: Endpoint<PatchProductImageRequest>
{
	public override void Configure()
	{
		Patch("image");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Change Image")
			.WithDescription("Change your Product's Image")
		);
	}

	public override async Task HandleAsync(PatchProductImageRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new SetProductImageCommand(
				Id: ProductId.New(req.Id),
				ContentType: req.ContentType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
