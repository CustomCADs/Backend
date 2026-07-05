
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldNotThrowException_WhenCustomValid(string name)
	{
		CreateCustom().SetName(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetName_ShouldPopulateProperties(string name)
	{
		var Custom = CreateCustom();
		Custom.SetName(name);
		await Assert.That(Custom.Name).IsEqualTo(name);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => CreateCustom().SetName(name));
	}
}
