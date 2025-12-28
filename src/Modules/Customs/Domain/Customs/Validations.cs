namespace CustomCADs.Modules.Customs.Domain.Customs;

using static CustomConstants;

internal static class Validations
{
	extension(Custom custom)
	{
		internal Custom ValidateName()
			=> custom
				.ThrowIfNull(
					expression: x => x.Name,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: x => x.Name,
					length: (NameMinLength, NameMaxLength)
				);

		internal Custom ValidateDescription()
			=> custom
				.ThrowIfNull(
					expression: x => x.Description,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: x => x.Description,
					length: (DescriptionMinLength, DescriptionMaxLength)
				);
	}

}
