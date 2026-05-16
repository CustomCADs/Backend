using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;

public class AccountDeletedHandler(IUserService service)
{
	public async Task HandleAsync(AccountDeletedApplicationEvent @event)
	{
		await service.DeleteAsync(@event.Id).ConfigureAwait(false);
	}
}
