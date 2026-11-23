using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetCountry;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Country;

public class GetCountryEndpoint(IRequestSender sender)
	: Endpoint<GetCountryRequest, GetCountryResponse>
{
	public override void Configure()
	{
		Get("country/{name}");
		Group<ShipmentsGroup>();
		Description(x => x
			.WithSummary("Country")
			.WithDescription("See the CountryId for any valid Country")
		);
	}

	public override async Task HandleAsync(GetCountryRequest req, CancellationToken ct)
	{
		int id = await sender.SendQueryAsync(
			query: new GetCountryQuery(req.Name),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(new GetCountryResponse(id)).ConfigureAwait(false);
	}
}
