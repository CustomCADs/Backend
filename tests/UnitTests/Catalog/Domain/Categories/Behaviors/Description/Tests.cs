using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

public class Tests : Data.Categories.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
	{
		var category = CreateCategory();

		category.SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
	{
		var category = CreateCategory();

		category.SetDescription(description);

		Assert.Equal(category.Description, description);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
	{
		var category = CreateCategory();

		Assert.Throws<CustomValidationException<Category>>(
			() => category.SetDescription(description)
		);
	}
}
