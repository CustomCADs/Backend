using CustomCADs.Shared.API;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Currencies;

namespace CustomCADs.Presentation;

using static APIConstants;

public static class ExchangeRatesEndpoint
{
	extension(IEndpointRouteBuilder app)
	{
		public void MapExchangeRatesEndpoint() =>
			app.MapGet(
				pattern: $"api/v1/{Paths.ExchangeRates}",
				handler: async (ICurrencyService service, ICacheService cache, CancellationToken ct = default) =>
				{
					IReadOnlyCollection<ExchangeRate> rates = await cache.GetOrCreateAsync(
						key: ICurrencyService.ExchangeRatesCacheKey,
						factory: service.GetRatesAsync
					).ConfigureAwait(false) ?? [];

					ExchangeRate[] response = [.. rates];
					return Results.Ok(response);
				}
			)
			.WithTags(Tags[Paths.ExchangeRates])
			.WithSummary("Get Exchange Rates")
			.WithDescription("Updates every 24h");
	}
}
