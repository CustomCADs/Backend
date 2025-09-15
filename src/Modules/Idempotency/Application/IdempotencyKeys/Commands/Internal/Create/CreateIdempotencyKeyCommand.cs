namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;

public sealed record CreateIdempotencyKeyCommand(
	Guid IdempotencyKey,
	string RequestHash
) : ICommand<IdempotencyKeyId>;
