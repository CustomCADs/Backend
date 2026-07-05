
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldNotThrowException_WhenCustomValid(string description)
	{
		CreateCustom().SetDescription(description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetDescription_ShouldPopulateProperties(string description)
	{
		var custom = CreateCustom();
		custom.SetDescription(description);
		await Assert.That(custom.Description).IsEqualTo(description);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => CreateCustom().SetDescription(description));
	}
}
