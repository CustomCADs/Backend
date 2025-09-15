namespace CustomCADs.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

public sealed record GetIdempotencyKeyDto(
	string ResponseBody,
	int StatusCode
);
