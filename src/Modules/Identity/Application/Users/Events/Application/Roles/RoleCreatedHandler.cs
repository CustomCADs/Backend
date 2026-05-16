using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;

public class RoleCreatedHandler(IRoleService service)
{
	public async Task HandleAsync(RoleCreatedApplicationEvent @event)
		=> await service.CreateAsync(
			name: @event.Name
		).ConfigureAwait(false);
}
