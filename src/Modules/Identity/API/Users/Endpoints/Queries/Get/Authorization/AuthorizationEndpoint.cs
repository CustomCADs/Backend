namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Queries.Get.Authorization;

public sealed class AuthorizationEndpoint
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Get("authorization");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("AuthZ")
			.WithDescription("See what Role you're logged in with")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await Send.OkAsync(User.Authorization).ConfigureAwait(false);
	}
}
