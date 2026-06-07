using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;

public class ProductViewedHandler(IAccountWrites writes, IUnitOfWork uow)
{
	public async Task HandleAsync(ProductViewedApplicationEvent @event)
	{
		await writes.ViewProductAsync(@event.AccountId, @event.Id, @event.ViewedAt).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
