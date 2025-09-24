using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Single;

public sealed class GetCustomEndpoint(IRequestSender sender)
	: Endpoint<GetCustomRequest, GetCustomResponse, GetCustomsStatsMapper>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Custom")
		);
	}

	public override async Task HandleAsync(GetCustomRequest req, CancellationToken ct)
	{
		CustomerGetCustomByIdDto custom = await sender.SendQueryAsync(
			query: new CustomerGetCustomByIdQuery(
				Id: CustomId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(custom, Map.FromEntity).ConfigureAwait(false);
	}
}
