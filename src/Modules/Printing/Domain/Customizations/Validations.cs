namespace CustomCADs.Modules.Printing.Domain.Customizations;

using static CustomizationConstants;

internal static class Validations
{
	extension(Customization customization)
	{
		internal Customization ValidateInfill()
			=> customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Infill,
					range: (InfillMin, InfillMax)
				);

		internal Customization ValidateVolume()
		{
			customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Volume.Value,
					range: (VolumeMin, VolumeMax)
				);

			customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Volume.Base,
					range: (BaseMin, BaseMax)
				);

			customization
				.ThrowIfInvalidRange(
					expression: (x) => x.Volume.Scale,
					range: (ScaleMin, ScaleMax)
				);

			return customization;
		}

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
