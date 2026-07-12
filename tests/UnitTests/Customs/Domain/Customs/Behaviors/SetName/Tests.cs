
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public void SetName_ShouldNotThrowException_WhenNameValid()
	{
		CreateCustom().SetName(MaxValidName);
		CreateCustom().SetName(MinValidName);
	}

	[Test]
	public async Task SetName_ShouldPopulateProperties()
	{
		var custom = CreateCustom();
		custom.SetName(MaxValidName);
		await Assert.That(custom.Name).IsEqualTo(MaxValidName);
	}

	[Test]
	[Arguments(InvalidName)]
	[Arguments(null)]
	public void SetName_ShouldThrowException_WhenNameInvalid(string? name)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => CreateCustom().SetName(name!));
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldThrowException_WhenInvalidStatus(Custom custom)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => custom.SetName(MaxValidName));
	}
}
