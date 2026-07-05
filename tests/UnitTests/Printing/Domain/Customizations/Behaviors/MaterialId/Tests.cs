using CustomCADs.Modules.Printing.Domain.Customizations;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.MaterialId;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetMaterialId_ShouldNotThrowException()
	{
		CreateCustomization().SetMaterialId(ValidMaterialId);
	}

	[Test]
	public async Task SetMaterialId_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetMaterialId(ValidMaterialId);

		await Assert.That(material.MaterialId).IsEqualTo(ValidMaterialId);
	}
}
