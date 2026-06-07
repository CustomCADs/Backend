
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

public class Tests : Data.Customs.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldNotThrowException_WhenCustomValid(string name)
	{
		CreateCustom().SetName(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldPopulateProperties(string name)
	{
		var Custom = CreateCustom();
		Custom.SetName(name);
		Assert.Equal(name, Custom.Name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetName_ShouldThrowException_WhenNameInvalid(string name)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom().SetName(name)
		);
	}
}
