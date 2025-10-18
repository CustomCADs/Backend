using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;

public class ProductViewedHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
{
	public async Task HandleAsync(ProductViewedApplicationEvent ae)
	{
		Product product = await reads.SingleByIdAsync(ae.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(ae.Id);

		string username = await sender.SendQueryAsync(
			query: new GetUsernameByIdQuery(ae.AccountId)
		).ConfigureAwait(false);

		(DateTimeOffset _, bool UserTracksViewedProducts, string? _, string? _) = await sender.SendQueryAsync(
			query: new GetAccountInfoByUsernameQuery(username)
		).ConfigureAwait(false);

		if (!UserTracksViewedProducts)
		{
			return;
		}

		bool userAlreadyViewed = await sender.SendQueryAsync(
			query: new GetAccountViewedProductQuery(ae.AccountId, ae.Id)
		).ConfigureAwait(false);

		if (userAlreadyViewed)
		{
			return;
		}

		product.AddToViewCount();
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			@event: new UserViewedProductApplicationEvent(
				Id: ae.Id,
				AccountId: ae.AccountId
			)
		).ConfigureAwait(false);
	}
}
