namespace CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;

using static IdempotencyKeyConstants;

internal static class Validations
{
	extension(IdempotencyKey idempotencyKey)
	{
		internal IdempotencyKey ValidateIdempotencyKey()
			=> idempotencyKey
				.ThrowIfNull(
					expression: (x) => x.Id,
					predicate: (x) => x.IsEmpty()
				);

		internal IdempotencyKey ValidateRequestHash()
			=> idempotencyKey
				.ThrowIfNull(
					expression: (x) => x.RequestHash,
					predicate: string.IsNullOrWhiteSpace
				);

		internal IdempotencyKey ValidateResponseBody()
			=> idempotencyKey
				.ThrowIfNull(
					expression: (x) => x.ResponseBody,
					predicate: (x) => x is null
				);

		internal IdempotencyKey ValidateStatusCode()
			=> idempotencyKey
				.ThrowIfNull(
					expression: (x) => x.StatusCode,
					predicate: (x) => x is null
				)
				.ThrowIfInvalidRange(
					expression: (x) => (int)x.StatusCode!,
					range: (MinStatusCode, MaxStatusCode),
					property: nameof(idempotencyKey.StatusCode)
				);
	}

}
