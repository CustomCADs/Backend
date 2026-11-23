using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;

public class UserEditedHandler(IAccountReads reads, IUnitOfWork uow)
	: IEventHandler<UserEditedApplicationEvent>
{
	public async Task HandleAsync(UserEditedApplicationEvent @event)
	{
		Account account = await reads.SingleByIdAsync(@event.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(@event.Id);

		if (@event.Username is not null)
		{
			if (@event.Username != account.Username)
			{
				account.SetUsername(@event.Username);
			}
		}

		if (@event.TrackViewedProducts is not null)
		{
			account.SetTrackViewedProducts(@event.TrackViewedProducts.Value);
		}

		if (@event.Names is not null)
		{
			if (@event.Names.FirstName != account.FirstName)
			{
				account.SetFirstName(@event.Names.FirstName);
			}
			if (@event.Names.LastName != account.LastName)
			{
				account.SetLastName(@event.Names.LastName);
			}
		}

		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
