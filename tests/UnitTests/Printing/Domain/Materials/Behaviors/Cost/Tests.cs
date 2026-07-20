using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Cost;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Test]
	public void SetCost_ShouldNotThrowException()
	{
		CreateMaterial().SetCost(MaxValidCost);
	}

	[Test]
	public async Task SetCost_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetCost(MaxValidCost);

		await Assert.That(material.Cost).IsEqualTo(MaxValidCost);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetCost_ShouldThrowException_WhenCostInvalid(decimal cost)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(() => material.SetCost(cost));
	}
}
