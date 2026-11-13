using CustomCADs.Modules.Catalog.API.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetAll;

namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.All;

public class GetAllTagsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<TagResponse[]>
{
	public override void Configure()
	{
		Get("");
		Group<TagGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("All")
			.WithDescription("Get Tags")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		TagDto[] tags = await sender.SendQueryAsync(
			query: new GetAllTagsQuery(),
			ct: ct
		).ConfigureAwait(false);

		TagResponse[] response = [.. tags.Select(x => x.ToTagResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
