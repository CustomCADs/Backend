using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.Sortings;

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
