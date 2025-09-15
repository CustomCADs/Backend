namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;

public sealed record DeleteIdempotencyKeyCommand(
	Guid IdempotencyKey,
	string RequestHash
) : ICommand;
