using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

namespace CustomCADs.Catalog.API.Tags.Endpoints.Put;

public class EditTagEndpoint(IRequestSender sender)
	: Endpoint<EditTagRequest>
{
	public override void Configure()
	{
		Put("");
		Group<TagGroup>();
		Description(x => x
			.WithSummary("Edit")
			.WithDescription("Edit Tag")
		);
	}

	public override async Task HandleAsync(EditTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditTagCommand(
				Id: TagId.New(req.Id),
				Name: req.Name
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
