using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Account.Accounts;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductViewed;

public class ProductViewedHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
{
	public async Task HandleAsync(ProductViewedApplicationEvent @event)
	{
		bool userAlreadyViewed = await sender.SendQueryAsync(
			query: new GetAccountViewedProductQuery(@event.AccountId, @event.Id)
		).ConfigureAwait(false);
		if (userAlreadyViewed) return;

		var account = await sender.SendQueryAsync(
			query: new GetAccountInfoByUsernameQuery(
				Username: await sender.SendQueryAsync(
					query: new GetUsernameByIdQuery(@event.AccountId)
				).ConfigureAwait(false)
			)
		).ConfigureAwait(false);

		if (!account.TrackViewedProducts) return;

		Product product = await reads.SingleByIdAsync(@event.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(@event.Id);

		product.AddToViewCount();
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserViewedProductApplicationEvent(
				Id: @event.Id,
				AccountId: @event.AccountId,
				ViewedAt: @event.ViewedAt
			)
		).ConfigureAwait(false);
	}
}
