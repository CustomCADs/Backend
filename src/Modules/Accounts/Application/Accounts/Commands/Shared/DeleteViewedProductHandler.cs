using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Commands.Shared;

public class DeleteViewedProductHandler(IAccountWrites writes, IUnitOfWork uow) : ICommandHandler<DeleteViewedProductCommand>
{
	public async Task Handle(DeleteViewedProductCommand req, CancellationToken ct = default)
	{
		await writes.UnviewProductAsync(req.CallerId, req.ProductId, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
