using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Create.WithId;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowException()
	{
		CreateMaterial();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		Material material = CreateMaterial(MaxValidName, MaxValidDensity, MaxValidCost, ValidTextureId, ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(material.Id).IsEqualTo(ValidId);
			await Assert.That(material.Name).IsEqualTo(MaxValidName);
			await Assert.That(material.Density).IsEqualTo(MaxValidDensity);
			await Assert.That(material.Cost).IsEqualTo(MaxValidCost);
			await Assert.That(material.TextureId).IsEqualTo(ValidTextureId);
		}
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowExcetion_WhenInvalid(string name, decimal density, decimal cost)
	{
		Assert.Throws<CustomValidationException<Material>>(() => CreateMaterial(name, density, cost));
	}
}
