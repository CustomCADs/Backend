using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;

public class RoleDeletedHandler(IRoleService service)
{
	public async Task HandleAsync(RoleDeletedApplicationEvent @event)
		=> await service.DeleteAsync(
			name: @event.Name
		).ConfigureAwait(false);
}
