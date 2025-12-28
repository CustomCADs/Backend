using CustomCADs.Modules.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Accounts.API.Roles.Endpoints.Get.Single;

public sealed class GetRoleEndpoint(IRequestSender sender)
	: Endpoint<GetRoleRequest, RoleResponse>
{
	public override void Configure()
	{
		Get("{name}");
		Group<RolesGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See a Role in detail")
		);
	}

	public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
	{
		RoleDto role = await sender.SendQueryAsync(
			query: new GetRoleByIdQuery(RoleId.New(req.Id)),
			ct: ct
		).ConfigureAwait(false);

		RoleResponse response = role.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
