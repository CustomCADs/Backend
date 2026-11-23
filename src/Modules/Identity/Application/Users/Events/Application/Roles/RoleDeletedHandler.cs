using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Roles;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Roles;

public class RoleDeletedHandler(IRoleService service) : IEventHandler<RoleDeletedApplicationEvent>
{
	public async Task HandleAsync(RoleDeletedApplicationEvent @event)
		=> await service.DeleteAsync(
			name: @event.Name
		).ConfigureAwait(false);
}
