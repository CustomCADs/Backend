using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Accounts.API.Accounts.Dtos;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Accounts.API.Accounts.Endpoints.Get.All;

public sealed class GetAccountsEndpoint(IRequestSender sender)
	: Endpoint<GetAccountsRequest, Result<AccountResponse>, GetAccountsMapper>
{
	public override void Configure()
	{
		Get("");
		Group<AccountsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Accounts with Search, Sort and Pagination options")
		);
	}

	public override async Task HandleAsync(GetAccountsRequest req, CancellationToken ct)
	{
		Result<GetAllAccountsDto> result = await sender.SendQueryAsync(
			query: new GetAllAccountsQuery(
				Username: req.Name,
				Sorting: new(req.SortingType, req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(result, Map.FromEntity).ConfigureAwait(false);
	}
}
