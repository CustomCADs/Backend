using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;
using CustomCADs.Accounts.Domain.Accounts.Enums;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Sortings;

public sealed class GetAccountSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<AccountSortingType[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<AccountsGroup>();
		Description(x => x
			.WithSummary("Sortings")
			.WithDescription("See all Account Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		AccountSortingType[] result = await sender.SendQueryAsync(
			query: new GetAccountSortingsQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
