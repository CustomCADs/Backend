using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;
using CustomCADs.Accounts.Endpoints.Accounts.Dtos;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Single;

public sealed class GetAccountEndpoint(IRequestSender sender)
	: Endpoint<GetAccountRequest, AccountResponse>
{
	public override void Configure()
	{
		Get("{username}");
		Group<AccountsGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See an Account in detail")
		);
	}

	public override async Task HandleAsync(GetAccountRequest req, CancellationToken ct)
	{
		GetAccountByIdDto account = await sender.SendQueryAsync(
			query: new GetAccountByIdQuery(AccountId.New(req.Id)),
			ct: ct
		).ConfigureAwait(false);

		AccountResponse response = account.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
