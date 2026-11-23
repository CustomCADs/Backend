namespace CustomCADs.Modules.Printing.Domain.Customizations.ValueObjects;

public record Volume(decimal Value = 0m, decimal Base = 0m, decimal Scale = 0m)
{
	private (decimal Base, decimal Scale) Cubed => (
		Base: Base * Base * Base,
		Scale: Scale * Scale * Scale
	);
	public decimal InMm3 => Value * Cubed.Base * Cubed.Scale;
	public decimal InCm3 => InMm3 / 1000;
}
