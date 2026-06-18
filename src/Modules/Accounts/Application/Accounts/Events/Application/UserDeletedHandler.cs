using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;

public class UserDeletedHandler(
	IAccountReads reads,
	IAccountWrites writes,
	IUnitOfWork uow
) : IEventHandler<UserDeletedApplicationEvent>
{
	public async Task HandleAsync(UserDeletedApplicationEvent @event)
	{
		Account account = await reads.SingleByIdAsync(@event.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(@event.Id);

		writes.Remove(account);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
