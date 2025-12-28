namespace CustomCADs.Modules.Printing.Domain.Customizations;

using static CustomizationConstants;

internal static class Validations
{
	extension(Customization customization)
	{
		internal Customization ValidateScale()
			=> customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Scale,
					range: (ScaleMin, ScaleMax)
				);

		internal Customization ValidateInfill()
			=> customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Infill,
					range: (InfillMin, InfillMax)
				);

		internal Customization ValidateVolume()
			=> customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Volume,
					range: (VolumeMin, VolumeMax)
				);

		internal Customization ValidateColor()
			=> customization
				.ThrowIfNull(
					expression: (x) => x.Color,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfPredicateIsFalse(
					expression: (x) => x.Color,
					predicate: Color.IsMatch,
					message: "A {0} must have a proper {1}."
				);
	}

}
