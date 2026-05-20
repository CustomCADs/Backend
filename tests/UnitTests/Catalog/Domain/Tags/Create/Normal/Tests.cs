
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.Normal;

public class Tests : Data.Tags.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name)
	{
		Tag.Create(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenProductIsValid(string name)
	{
		Tag tag = Tag.Create(name);

		Assert.Equal(name, tag.Name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => Tag.Create(name)
		);
	}
}
