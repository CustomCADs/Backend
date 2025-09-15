using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.All;

public class GetAllTagsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<GetAllTagsResponse[]>
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

		GetAllTagsResponse[] response = [.. tags.Select(x => x.ToGetAllTagsResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
