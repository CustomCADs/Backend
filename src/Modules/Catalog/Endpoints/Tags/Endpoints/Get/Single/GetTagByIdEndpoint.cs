using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.Single;

public class GetTagByIdEndpoint(IRequestSender sender)
	: Endpoint<GetTagByIdRequest, GetTagByIdResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<TagGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("Get Tag")
		);
	}

	public override async Task HandleAsync(GetTagByIdRequest req, CancellationToken ct)
	{
		TagDto tag = await sender.SendQueryAsync(
			query: new GetTagByIdQuery(
				Id: TagId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		GetTagByIdResponse response = tag.ToGetTagByIdResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
