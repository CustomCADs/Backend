using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Create.Normal;

public class Tests : Data.Categories.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenCategoryIsValid(string name, string description)
	{
		Category.Create(name, description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties_WhenCategoryIsValid(string name, string description)
	{
		var category = Category.Create(name, description);

		using (Assert.Multiple())
		{
			await Assert.That(name).IsEqualTo(category.Name);
			await Assert.That(description).IsEqualTo(category.Description);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenCategoryIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Category>>(() => Category.Create(name, description));
	}
}
