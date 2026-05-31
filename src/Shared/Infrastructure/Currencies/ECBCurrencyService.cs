using CustomCADs.Shared.Application.Currencies;
using CustomCADs.Shared.Infrastructure.Utilities;

namespace CustomCADs.Shared.Infrastructure.Currencies;

public class ECBCurrencyService(HttpClient client) : ICurrencyService
{
	public async Task<IReadOnlyCollection<ExchangeRate>> GetRatesAsync(CancellationToken ct)
	{
		HttpResponseMessage response = await SendRequestAsync().ConfigureAwait(false);
		Gesmes.Envelope envelope = await response.Content.ReadAsXmlAsync<Gesmes.Envelope>(ct).ConfigureAwait(false);

		return [.. envelope.Cube.TimeCube.ToExchangeRates()];
	}

	private async Task<HttpResponseMessage> SendRequestAsync(string resource = "eurofxref-daily.xml")
	{
		HttpResponseMessage response = await client.GetAsync(
			$"/stats/eurofxref/{resource}"
		).ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
		return response;
	}
}
