using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.API.Accounts.Endpoints.Delete;

public sealed class DeleteAccountEndpoint(IRequestSender sender)
	: Endpoint<DeleteAccountRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<AccountsGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete an Account")
		);
	}

	public override async Task HandleAsync(DeleteAccountRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteAccountCommand(AccountId.New(req.Id)),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
