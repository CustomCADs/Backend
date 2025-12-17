using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;

public sealed class DecreaseActiveCartItemQuantityHandler(
	IActiveCartReads reads,
	IUnitOfWork uow
) : ICommandHandler<DecreaseActiveCartItemQuantityCommand, int>
{
	public async Task<int> Handle(DecreaseActiveCartItemQuantityCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.CallerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.CallerId, req.ProductId }); ;

		item.DecreaseQuantity(req.Amount);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return item.Quantity;
	}
}
