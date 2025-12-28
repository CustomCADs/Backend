namespace CustomCADs.Modules.Printing.Domain.Materials;

using static MaterialConstants;

internal static class Validations
{
	extension(Material material)
	{
		internal Material ValidateName()
			=> material
				.ThrowIfNull(
					expression: (x) => x.Name,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: (x) => x.Name,
					length: (NameMinLength, NameMaxLength)
				);

		internal Material ValidateDensity()
			=> material
				.ThrowIfInvalidRange(
					expression: (x) => x.Density,
					range: (DensityMin, DensityMax)
				);

		internal Material ValidateCost()
			=> material
				.ThrowIfInvalidRange(
					expression: (x) => x.Cost,
					range: (CostMin, CostMax)
				);
	}

}
