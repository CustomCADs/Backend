namespace CustomCADs.Modules.Catalog.Domain.Tags;

using static TagConstants;

internal static class Validations
{
	extension(Tag tag)
	{
		internal Tag ValidateName()
			=> tag
				.ThrowIfNull(
					expression: x => x.Name,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: x => x.Name,
					length: (NameMinLength, NameMaxLength)
				);
	}

}
