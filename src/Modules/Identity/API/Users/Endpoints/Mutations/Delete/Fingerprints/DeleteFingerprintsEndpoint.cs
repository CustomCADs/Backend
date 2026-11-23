using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete.Fingerprints;
using CustomCADs.Shared.Domain.TypedIds.Identity;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Delete.Fingerprints;

public sealed class DeleteFingerprintsEndpoint(IRequestSender sender)
	: Endpoint<DeleteFingerprintsRequest>
{
	public override void Configure()
	{
		Delete("fingerprint");
		Group<IdentityGroup>();
		Description(x => x
			.WithSummary("Remove Fingerprint")
			.WithDescription("Remove a Fingerprint")
		);
	}

	public override async Task HandleAsync(DeleteFingerprintsRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteFingerprintCommand(
				RefreshTokenId: RefreshTokenId.New(req.RefreshTokenId),
				CurrentRefreshToken: HttpContext.RefreshTokenCookie,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}

