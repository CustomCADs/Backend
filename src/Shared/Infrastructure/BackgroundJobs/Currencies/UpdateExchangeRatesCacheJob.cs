using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Currencies;
using Quartz;

namespace CustomCADs.Shared.Infrastructure.BackgroundJobs.Currencies;

public class UpdateExchangeRatesCacheJob(ICurrencyService service, ICacheService cache) : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		await cache.SetAsync(
			key: ICurrencyService.ExchangeRatesCacheKey,
			item: await service.GetRatesAsync().ConfigureAwait(false)
		).ConfigureAwait(false);
	}
}
