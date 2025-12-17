namespace CustomCADs.Modules.Catalog.Domain.Categories;

using static CategoryConstants;

internal static class Validations
{
	extension(Category category)
	{
		internal Category ValidateName()
			=> category
				.ThrowIfNull(
					expression: x => x.Name,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: x => x.Name,
					length: (NameMinLength, NameMaxLength)
				);

		internal Category ValidateDescription()
			=> category
				.ThrowIfNull(
					expression: x => x.Description,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: x => x.Description,
					length: (DescriptionMinLength, DescriptionMaxLength)
				);
	}

}

