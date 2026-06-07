using CustomCADs.Modules.Printing.Domain.Customizations;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.MaterialId;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Fact]
	public void SetMaterialId_ShouldNotThrowException()
	{
		CreateCustomization().SetMaterialId(ValidMaterialId);
	}

	[Fact]
	public void SetMaterialId_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetMaterialId(ValidMaterialId);

		Assert.Equal(ValidMaterialId, material.MaterialId);
	}
}
