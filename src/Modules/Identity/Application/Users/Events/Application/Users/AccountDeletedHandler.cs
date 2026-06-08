using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;

public class AccountDeletedHandler(IUserService service)
	: IEventHandler<AccountDeletedApplicationEvent>
{
	public async Task HandleAsync(AccountDeletedApplicationEvent @event)
	{
		await service.DeleteAsync(@event.Id).ConfigureAwait(false);
	}
}
