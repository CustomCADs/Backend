using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

namespace CustomCADs.Customs.API.Customs.Endpoints.Designer.Get.Single;

public sealed class GetCustomEndpoint(IRequestSender sender)
	: Endpoint<GetCustomRequest, GetCustomResponse, GetCustomMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See an Accepted by You or a Pending Custom")
		);
	}

	public override async Task HandleAsync(GetCustomRequest req, CancellationToken ct)
	{
		DesignerGetCustomByIdDto custom = await sender.SendQueryAsync(
			query: new DesignerGetCustomByIdQuery(
				Id: CustomId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(custom, Map.FromEntity).ConfigureAwait(false);
	}
}
