namespace CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public sealed record GetIdempotencyKeyQuery(
	Guid IdempotencyKey,
	string RequestHash
) : IQuery<GetIdempotencyKeyDto?>;
