using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Printing.Domain.Customizations.Behaviors.Scale;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	[Test]
	public void SetScale_ShouldNotThrowException()
	{
		CreateCustomization().SetScale(MaxValidScale);
	}

	[Test]
	public async Task SetScale_ShouldPopulateProperties()
	{
		Customization material = CreateCustomization();

		material.SetScale(MaxValidScale);

		await Assert.That(material.Scale).IsEqualTo(MaxValidScale);
	}

	[Test]
	[MethodDataSource(typeof(TestData), nameof(ITheoryData<>.GetTestData))]
	public void SetScale_ShouldThrowException_WhenScaleInvalid(decimal scale)
	{
		Customization material = CreateCustomization();

		Assert.Throws<CustomValidationException<Customization>>(() => material.SetScale(scale));
	}
}
