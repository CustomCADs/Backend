using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandler(IAccountWrites writes, IUnitOfWork uow)
	: IEventHandler<UserViewedProductApplicationEvent>
{
	public async Task HandleAsync(UserViewedProductApplicationEvent @event)
	{
		await writes.ViewProductAsync(@event.AccountId, @event.Id, @event.ViewedAt).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
