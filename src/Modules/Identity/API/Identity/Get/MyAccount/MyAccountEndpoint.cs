using CustomCADs.Modules.Identity.Application.Users.Queries.Internal.GetByUsername;

namespace CustomCADs.Modules.Identity.API.Identity.Get.MyAccount;

public sealed class MyAccountEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<MyAccountResponse, MyAccountMapper>
{
	public override void Configure()
	{
		Get("my-account");
		Group<IdentityGroup>();
		Description(x => x
			.WithSummary("My Account")
			.WithDescription("See your Account's details")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		GetUserByUsernameDto user = await sender.SendQueryAsync(
			query: new GetUserByUsernameQuery(User.AccountId, HttpContext.RefreshTokenCookie),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(user, Map.FromEntity).ConfigureAwait(false);
	}
}
