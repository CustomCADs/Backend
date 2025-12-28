namespace CustomCADs.Modules.Identity.API.Identity.Get.Authentication;

public sealed class AuthenticationEndpoint
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Get("authentication");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithName(IdentityNames.Authentication)
			.WithSummary("AuthN")
			.WithDescription("See if you're logged in")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await Send.OkAsync(User.IsAuthenticated).ConfigureAwait(false);
	}
}
