using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;

public sealed class IncreaseActiveCartItemQuantityHandler(
	IActiveCartReads reads,
	IUnitOfWork uow
) : ICommandHandler<IncreaseActiveCartItemQuantityCommand, int>
{
	public async Task<int> Handle(IncreaseActiveCartItemQuantityCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.CallerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.CallerId, req.ProductId }); ;

		item.IncreaseQuantity(req.Amount);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return item.Quantity;
	}
}
