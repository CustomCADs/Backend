using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Users;

public class AccountCreatedHandler(IUserService service)
{
	public async Task HandleAsync(AccountCreatedApplicationEvent @event)
	{
		await service.CreateAsync(
			user: User.Create(
				role: @event.Role,
				username: @event.Username,
				email: new(@event.Email),
				accountId: @event.Id
			),
			password: @event.Password
		).ConfigureAwait(false);
	}
}
