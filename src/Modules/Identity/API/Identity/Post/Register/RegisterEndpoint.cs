using CustomCADs.Identity.Application.Users.Commands.Internal.Register;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Shared.API.Attributes;

namespace CustomCADs.Identity.API.Identity.Post.Register;

public sealed class RegisterEndpoint(IRequestSender sender)
	: Endpoint<RegisterRequest>
{
	public override void Configure()
	{
		Post("register");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.Register)
			.WithSummary("Register")
			.WithDescription("Register an Account")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new RegisterUserCommand(
				Role: req.Role,
				Username: req.Username,
				Email: req.Email,
				Password: req.Password,
				FirstName: req.FirstName,
				LastName: req.LastName
			),
			ct: ct
		).ConfigureAwait(false);

		await sender.SendCommandAsync(
			command: new VerificationEmailCommand(
				Username: req.Username
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
