using CustomCADs.Modules.Catalog.API.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetByName;

namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.Single.ByName;

public class GetTagByNameEndpoint(IRequestSender sender)
	: Endpoint<GetTagByNameRequest, TagResponse>
{
	public override void Configure()
	{
		Get("{name:alpha}");
		Group<TagGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Single by Name")
			.WithDescription("Get Tag by specifying its Name")
		);
	}

	public override async Task HandleAsync(GetTagByNameRequest req, CancellationToken ct)
	{
		TagDto tag = await sender.SendQueryAsync(
			query: new GetTagByNameQuery(req.Name),
			ct: ct
		).ConfigureAwait(false);

		TagResponse response = tag.ToTagResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
