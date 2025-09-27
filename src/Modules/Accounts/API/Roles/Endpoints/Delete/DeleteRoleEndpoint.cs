using CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.API.Roles.Endpoints.Delete;

public sealed class DeleteRoleEndpoint(IRequestSender sender)
	: Endpoint<DeleteRoleRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<RolesGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete a Role")
		);
	}

	public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteRoleCommand(RoleId.New(req.Id)),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
