using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Materials.Behaviors.Name;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	[Test]
	public void SetName_ShouldNotThrowException()
	{
		CreateMaterial().SetName(MaxValidName);
	}

	[Test]
	public async Task SetName_ShouldPopulateProperties()
	{
		Material material = CreateMaterial();

		material.SetName(MaxValidName);

		await Assert.That(material.Name).IsEqualTo(MaxValidName);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Material material = CreateMaterial();

		Assert.Throws<CustomValidationException<Material>>(() => material.SetName(name));
	}
}
