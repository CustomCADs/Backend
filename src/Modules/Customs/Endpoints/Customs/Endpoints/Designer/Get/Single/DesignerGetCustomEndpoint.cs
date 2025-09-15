using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;

public sealed class DesignerGetCustomEndpoint(IRequestSender sender)
	: Endpoint<DesignerGetCustomRequest, DesignerGetCustomResponse>
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

	public override async Task HandleAsync(DesignerGetCustomRequest req, CancellationToken ct)
	{
		DesignerGetCustomByIdDto custom = await sender.SendQueryAsync(
			query: new DesignerGetCustomByIdQuery(
				Id: CustomId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		DesignerGetCustomResponse response = custom.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
