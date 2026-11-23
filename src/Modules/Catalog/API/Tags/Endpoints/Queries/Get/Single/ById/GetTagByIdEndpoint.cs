using CustomCADs.Modules.Catalog.API.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Queries.Internal.GetById;

namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.Single.ById;

public class GetTagByIdEndpoint(IRequestSender sender)
	: Endpoint<GetTagByIdRequest, TagResponse>
{
	public override void Configure()
	{
		Get("{id:guid}");
		Group<TagGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Single by Id")
			.WithDescription("Get Tag by specifying its Id")
		);
	}

	public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
	{
		TagDto tag = await sender.SendQueryAsync(
			query: new GetTagByIdQuery(TagId.New(req.Id)),
			ct: ct
		).ConfigureAwait(false);

		TagResponse response = tag.ToTagResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
