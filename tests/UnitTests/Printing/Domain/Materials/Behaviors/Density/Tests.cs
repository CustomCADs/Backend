using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Density;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Fact]
	public void SetDensity_ShouldNotThrowException()
	{
		CreateMaterial().SetDensity(MaxValidDensity);
	}

	[Fact]
	public void SetDensity_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetDensity(MaxValidDensity);

		Assert.Equal(MaxValidDensity, material.Density);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void SetDensity_ShouldThrowException_WhenDensityInvalid(decimal density)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(
			() => material.SetDensity(density)
		);
	}
}
