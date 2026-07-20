using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add;

using static Data.ActiveCarts.TestData;

public class ValidData : ITheoryData<CustomizationId?>
{
	public static IEnumerable<CustomizationId?> GetTestData()
	{
		yield return ValidCustomizationId;
		yield return null;
	}
}
