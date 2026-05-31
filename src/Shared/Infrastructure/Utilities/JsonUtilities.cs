using Newtonsoft.Json;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class JsonUtilities
{
	extension<TDto>(HttpContent content) where TDto : class
	{
		public async Task<TDto> ReadAsJsonAsync(CancellationToken cancellationToken)
			=> JsonConvert.DeserializeObject<TDto>(
				value: await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)
			) ?? throw new JsonException($"Couldn't serialize {typeof(TDto).GetType()}");
	}

	extension<TDto>(TDto dto) where TDto : class
	{
		public string AsSerializedJson { get => JsonConvert.SerializeObject(dto); }
	}
}
