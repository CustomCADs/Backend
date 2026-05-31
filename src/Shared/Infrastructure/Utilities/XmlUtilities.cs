using System.Xml;
using System.Xml.Serialization;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class XmlUtilities
{
	private static XmlSerializer GetSerializer<TDto>() => new(type: typeof(TDto));

	extension<TDto>(HttpContent content) where TDto : class
	{
		public async Task<TDto> ReadAsXmlAsync(CancellationToken ct)
		{
			using Stream stream = await content.ReadAsStreamAsync(ct).ConfigureAwait(false);

			return GetSerializer<TDto>().Deserialize(stream) as TDto
				?? throw new XmlException($"Failed to parse XML to {typeof(TDto).GetType()}");
		}
	}

	extension<TDto>(TDto dto) where TDto : class
	{
		public string AsSerializedXml
		{
			get
			{
				System.Text.StringBuilder builder = new();
				using StringWriter writer = new(builder);

				GetSerializer<TDto>().Serialize(writer, dto);
				return builder.ToString();
			}
		}
	}
}
