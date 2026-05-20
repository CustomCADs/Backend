using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name)
	{
		CreateTag(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenProductIsValid(string name)
	{
		Tag tag = CreateTag(name, ValidId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, tag.Id),
			() => Assert.Equal(name, tag.Name)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(
			() => CreateTag(name)
		);
	}
}
