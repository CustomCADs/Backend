using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Modules.Identity.Application.Users.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Modules.Identity.API.Identity.Delete;

public sealed class DeleteAccountEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Delete("");
		Group<IdentityGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete your account")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteUserCommand(User.AccountId),
			ct: ct
		).ConfigureAwait(false);

		HttpContext.DeleteAllCookies(settings.Value.Domain);
	}
}
