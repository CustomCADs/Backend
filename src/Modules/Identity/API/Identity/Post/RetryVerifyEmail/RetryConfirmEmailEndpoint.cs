using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Shared.API.Attributes;

namespace CustomCADs.Modules.Identity.API.Identity.Post.RetryVerifyEmail;

public sealed class RetryConfirmEmailEndpoint(IRequestSender sender)
	: Endpoint<RetryConfirmEmailRequest>
{
	public override void Configure()
	{
		Post("email/confirm/retry");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.RetryConfirmEmail)
			.WithSummary("Retry Send Email")
			.WithDescription("Receive another verification email")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new VerificationEmailCommand(req.Username),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
