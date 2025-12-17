using System.Xml;
using System.Xml.Serialization;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class XmlUtilities
{
	private static XmlSerializer GetSerializer<TDto>() => new(type: typeof(TDto));

	extension(Stream stream)
	{
		public TDto DeserializeFromXml<TDto>() where TDto : class
			=> GetSerializer<TDto>().Deserialize(stream) as TDto
				?? throw new XmlException($"Failed to parse XML to {typeof(TDto).Name}");
	}

	extension<TDto>(TDto dto) where TDto : class
	{
		public void SerializeToXml(Stream stream) => GetSerializer<TDto>().Serialize(stream, dto);

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
