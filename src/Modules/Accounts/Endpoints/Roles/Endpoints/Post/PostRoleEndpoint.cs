using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.Single;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Post;

public sealed class PostRoleEndpoint(IRequestSender sender)
	: Endpoint<PostRoleRequest, RoleResponse>
{
	public override void Configure()
	{
		Post("");
		Group<RolesGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create a Role")
		);
	}

	public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
	{
		RoleId id = await sender.SendCommandAsync(
			command: new CreateRoleCommand(
				Name: req.Name,
				Description: req.Description
			),
			ct: ct
		).ConfigureAwait(false);

		RoleDto role = await sender.SendQueryAsync(
			query: new GetRoleByIdQuery(
				Id: id
			),
			ct: ct
		).ConfigureAwait(false);

		RoleResponse response = role.ToResponse();
		await Send.CreatedAtAsync<GetRoleEndpoint>(new { role.Name }, response).ConfigureAwait(false);
	}
}
