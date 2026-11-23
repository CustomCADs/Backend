using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Test]
	public void SetDensity_ShouldNotThrowException()
	{
		CreateMaterial().SetDensity(MaxValidDensity);
	}

	[Test]
	public async Task SetDensity_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetDensity(MaxValidDensity);

		await Assert.That(material.Density).IsEqualTo(MaxValidDensity);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetDensity_ShouldThrowException_WhenDensityInvalid(decimal density)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(() => material.SetDensity(density));
	}
}
