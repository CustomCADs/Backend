
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDescription;

public class Tests : Data.Customs.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldNotThrowException_WhenCustomValid(string description)
	{
		CreateCustom().SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldPopulateProperties(string description)
	{
		var custom = CreateCustom();
		custom.SetDescription(description);
		Assert.Equal(description, custom.Description);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom().SetDescription(description)
		);
	}
}
