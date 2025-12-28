namespace CustomCADs.Modules.Catalog.Domain.Products;

using static ProductConstants;

internal static class Validations
{
	extension(Product product)
	{
		internal Product ValidateName()
			=> product
				.ThrowIfNull(
					expression: x => x.Name,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: x => x.Name,
					length: (NameMinLength, NameMaxLength)
				);

		internal Product ValidateDescription()
			=> product
				.ThrowIfNull(
					expression: x => x.Description,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: x => x.Description,
					length: (DescriptionMinLength, DescriptionMaxLength)
				);

		internal Product ValidatePrice()
			=> product
				.ThrowIfInvalidRange(
					expression: x => x.Price,
					range: (PriceMin, PriceMax)
				);
	}

}
