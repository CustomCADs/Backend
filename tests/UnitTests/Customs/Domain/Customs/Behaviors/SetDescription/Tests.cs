
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionValid()
	{
		CreateCustom().SetDescription(MaxValidDescription);
		CreateCustom().SetDescription(MinValidDescription);
	}

	[Test]
	public async Task SetDescription_ShouldPopulateProperties()
	{
		var custom = CreateCustom();
		custom.SetDescription(MaxValidDescription);
		await Assert.That(custom.Description).IsEqualTo(MaxValidDescription);
	}

	[Test]
	[Arguments(InvalidDescription)]
	[Arguments(null)]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string? description)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => CreateCustom().SetDescription(description!));
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldThrowException_WhenInvalidStatus(Custom custom)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => custom.SetDescription(MaxValidDescription));
	}
}
