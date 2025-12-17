namespace CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;

public sealed record CompleteIdempotencyKeyCommand(
	IdempotencyKeyId Id,
	string RequestHash,
	string ResponseBody,
	int StatusCode
) : ICommand;
