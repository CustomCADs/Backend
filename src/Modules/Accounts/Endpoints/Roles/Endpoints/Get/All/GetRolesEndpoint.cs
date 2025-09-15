using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.All;

public sealed class GetRolesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<RoleResponse[]>
{
	public override void Configure()
	{
		Get("");
		Group<RolesGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Roles")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<RoleDto> roles = await sender.SendQueryAsync(
			query: new GetAllRolesQuery(),
			ct: ct
		).ConfigureAwait(false);

		RoleResponse[] response = [.. roles.Select(r => r.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
