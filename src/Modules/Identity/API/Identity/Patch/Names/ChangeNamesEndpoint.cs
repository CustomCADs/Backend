using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

namespace CustomCADs.Modules.Identity.API.Identity.Patch.Names;

public sealed class ChangeNamesEndpoint(IRequestSender sender)
	: Endpoint<ChangeNamesRequest>
{
	public override void Configure()
	{
		Patch("names");
		Group<IdentityGroup>();
		Description(x => x
			.WithSummary("Change Username")
			.WithDescription("Change your Username")
		);
	}

	public override async Task HandleAsync(ChangeNamesRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ChangeUsernameCommand(
				Id: User.AccountId,
				Username: req.Username,
				FirstName: req.FirstName,
				LastName: req.LastName
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
