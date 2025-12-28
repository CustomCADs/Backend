using Polly;

namespace CustomCADs.Shared.Infrastructure.Requests;

public static class ResilienceExtensions
{
	extension(PolicyBuilder builder)
	{
		public IAsyncPolicy AsyncRetry(int retryCount = 3, Func<int, TimeSpan>? sleepDurationProvider = null)
			=> builder.WaitAndRetryAsync(
				retryCount,
				sleepDurationProvider ?? (
					(attempt) => TimeSpan.FromSeconds(Math.Pow(2, attempt))
				)
			);

		public IAsyncPolicy AsyncCircuitBreak(int exceptionsAllowedBeforeBreaking = 2, TimeSpan? durationOfBreak = null)
				=> builder.CircuitBreakerAsync(
					exceptionsAllowedBeforeBreaking,
					durationOfBreak ?? TimeSpan.FromSeconds(30)
				);
	}
}
