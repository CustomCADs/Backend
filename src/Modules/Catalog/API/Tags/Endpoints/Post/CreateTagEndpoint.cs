using CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetById;

namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Post;

public class CreateTagEndpoint(IRequestSender sender)
	: Endpoint<CreateTagRequest, CreateTagResponse>
{
	public override void Configure()
	{
		Post("");
		Group<TagGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create Tag")
		);
	}

	public override async Task HandleAsync(CreateTagRequest req, CancellationToken ct)
	{
		TagId id = await sender.SendCommandAsync(
			command: new CreateTagCommand(
				Name: req.Name
			),
			ct: ct
		).ConfigureAwait(false);

		TagDto tag = await sender.SendQueryAsync(
			query: new GetTagByIdQuery(id),
			ct: ct
		).ConfigureAwait(false);

		CreateTagResponse response = tag.ToCreateTagResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
