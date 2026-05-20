
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

public class Tests : Data.Tags.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
	{
		CreateTag().SetName(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldPopulateProperties_WhenNameIsValid(string name)
	{
		var tag = CreateTag();
		tag.SetName(name);
		Assert.Equal(name, tag.Name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => CreateTag().SetName(name)
		);
	}
}
