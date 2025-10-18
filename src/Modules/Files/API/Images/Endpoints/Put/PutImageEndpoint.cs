using CustomCADs.Files.Application.Images.Commands.Internal.Edit;

namespace CustomCADs.Files.API.Images.Endpoints.Put;

public sealed class PutImageEndpoint(IRequestSender sender) : Endpoint<PutImageRequest>
{
	public override void Configure()
	{
		Put("");
		Group<ImagesGroup>();
		Description(x => x
			.WithSummary("Change")
			.WithDescription("Change your Image file")
		);
	}

	public override async Task HandleAsync(PutImageRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditImageCommand(
				Id: ImageId.New(req.Id),
				ContentType: req.ContentType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
