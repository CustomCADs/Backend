using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.WithId;

public class Tests : Data.Categories.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
	{
		CreateCategory(name, description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenCategoryIsValid(string name, string description)
	{
		var category = CreateCategory(name, description);

		Assert.Multiple(
			() => Assert.Equal(category.Name, name),
			() => Assert.Equal(category.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Category>>(
			() => CreateCategory(name, description)
		);
	}
}
