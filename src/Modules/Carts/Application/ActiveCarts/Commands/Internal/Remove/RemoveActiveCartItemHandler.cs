using CustomCADs.Modules.Carts.Domain.Repositories;
using CustomCADs.Modules.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Remove;

public sealed class RemoveActiveCartItemHandler(
	IActiveCartReads reads,
	IWrites<ActiveCartItem> writes,
	IUnitOfWork uow
) : ICommandHandler<RemoveActiveCartItemCommand>
{
	public async Task Handle(RemoveActiveCartItemCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.CallerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.CallerId, req.ProductId }); ;

		writes.Remove(item);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
