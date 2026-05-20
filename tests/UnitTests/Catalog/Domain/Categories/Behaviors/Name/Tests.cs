
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

public class Tests : Data.Categories.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
	{
		var category = CreateCategory();

		category.SetName(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_SetsName_WhenNameIsValid(string name)
	{
		var category = CreateCategory();

		category.SetName(name);

		Assert.Equal(category.Name, name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetName_ThrowsException_WhenNameIsInvalid(string name)
	{
		var category = CreateCategory();

		Assert.Throws<CustomValidationException<Category>>(
			() => category.SetName(name)
		);
	}
}
