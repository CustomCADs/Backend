using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;
using CustomCADs.Shared.API.Attributes;

namespace CustomCADs.Modules.Identity.API.Identity.Post.ForgotPassword;

public sealed class ForgotPasswordEndpoint(IRequestSender sender)
	: Endpoint<ForgotPasswordRequest>
{
	public override void Configure()
	{
		Post("password/forgot");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.ForgotPassword)
			.WithSummary("Reset Password Email")
			.WithDescription("Receive an Email with a link to reset your Password")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ResetPasswordEmailCommand(
				Email: req.Email
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
