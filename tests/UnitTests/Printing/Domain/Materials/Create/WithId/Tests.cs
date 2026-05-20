using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Create.WithId;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateMaterial();
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		Material material = CreateMaterial(MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId, ValidId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, material.Id),
			() => Assert.Equal(MaxValidName, material.Name),
			() => Assert.Equal(MaxValidDensity, material.Density),
			() => Assert.Equal(MaxValidCost, material.Cost),
			() => Assert.Equal(ValidTextureId, material.TextureId)
		);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void Create_ShouldThrowExcetion_WhenInvalid(string name, decimal density, decimal cost)
	{
		Assert.Throws<CustomValidationException<Material>>(
			() => CreateMaterial(name, density, cost)
		);
	}
}
