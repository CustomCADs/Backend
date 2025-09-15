using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Sortings;

public sealed class GetCustomSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<CustomSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Custom Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		CustomSortingType[] result = await sender.SendQueryAsync(
			query: new GetCustomSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
