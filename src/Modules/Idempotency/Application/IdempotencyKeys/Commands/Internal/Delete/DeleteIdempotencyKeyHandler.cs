using CustomCADs.Modules.Idempotency.Domain.Repositories;
using CustomCADs.Modules.Idempotency.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;

public sealed class DeleteIdempotencyKeyHandler(
	IIdempotencyKeyReads reads,
	IWrites<IdempotencyKey> writes,
	IUnitOfWork uow
) : ICommandHandler<DeleteIdempotencyKeyCommand>
{
	public async Task Handle(DeleteIdempotencyKeyCommand req, CancellationToken ct = default)
	{
		IdempotencyKey idempotencyKey = await reads.SingleByIdAsync(IdempotencyKeyId.New(req.IdempotencyKey), req.RequestHash, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<IdempotencyKey>.ById(new { req.IdempotencyKey, req.RequestHash });

		writes.Remove(idempotencyKey);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
