namespace CustomCADs.Modules.Files.Domain.Images;

internal static class Validations
{
	extension(Image image)
	{
		internal Image ValidateKey()
			=> image
				.ThrowIfNull(
					expression: x => x.Key,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Image ValidateContentType()
			=> image
				.ThrowIfNull(
					expression: x => x.ContentType,
					predicate: string.IsNullOrWhiteSpace
				);
	}

}
