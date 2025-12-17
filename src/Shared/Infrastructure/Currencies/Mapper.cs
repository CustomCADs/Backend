using CustomCADs.Shared.Application.Currencies;

namespace CustomCADs.Shared.Infrastructure.Currencies;

internal static class Mapper
{
	extension(Gesmes.CubeTime cubeTime)
	{
		internal IReadOnlyCollection<ExchangeRate> ToExchangeRates()
			=> [.. cubeTime.Rates.Select(cubeRate => new ExchangeRate(
				Date: cubeTime.Time,
				Currency: cubeRate.Currency,
				Rate: cubeRate.Rate
			))];
	}
}
