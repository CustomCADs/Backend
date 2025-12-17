namespace CustomCADs.Modules.Customs.Domain.Customs.States.Entities;

using static CustomConstants;

internal static class Validations
{
	extension(FinishedCustom custom)
	{
		internal FinishedCustom ValidatePrice()
			=> custom
				.ThrowIfInvalidRange(
					(x) => x.Price,
					(PriceMin, PriceMax)
				);
	}

}
