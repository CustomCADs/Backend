using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

namespace CustomCADs.Modules.Identity.API.Identity.Patch.ChangeUsername;

public sealed class ChangeUsernameEndpoint(IRequestSender sender)
	: Endpoint<ChangeUsernameRequest>
{
	public override void Configure()
	{
		Patch("username");
		Group<IdentityGroup>();
		Description(x => x
			.WithName(IdentityNames.ChangeUsername)
			.WithSummary("Change Username")
			.WithDescription("Change your Username")
		);
	}

	public override async Task HandleAsync(ChangeUsernameRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ChangeUsernameCommand(
				Username: User.Name,
				NewUsername: req.Username
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
