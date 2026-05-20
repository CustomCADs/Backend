using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add;

using static Data.ActiveCarts.TestData;

public class ValidData : TheoryData<CustomizationId?>
{
	public ValidData()
	{
		Add(ValidCustomizationId);
		Add(null);
	}
}
