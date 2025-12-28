using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandler(IAccountWrites writes, IUnitOfWork uow)
{
	public async Task HandleAsync(UserViewedProductApplicationEvent ae)
	{
		await writes.ViewProductAsync(ae.AccountId, ae.Id).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
