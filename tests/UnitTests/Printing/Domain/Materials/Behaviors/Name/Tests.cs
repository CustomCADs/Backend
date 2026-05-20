using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Fact]
	public void SetName_ShouldNotThrowException()
	{
		CreateMaterial().SetName(MaxValidName);
	}

	[Fact]
	public void SetName_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetName(MaxValidName);

		Assert.Equal(MaxValidName, material.Name);
	}

	[Theory]
	[ClassData(typeof(TestData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(
			() => material.SetName(name)
		);
	}
}
