using CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Delete;

namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Delete;

public class DeleteTagEndpoint(IRequestSender sender)
	: Endpoint<DeleteTagRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<TagGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete Tag")
		);
	}

	public override async Task HandleAsync(DeleteTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteTagCommand(
				Id: TagId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
