
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Name;

public class Tests : Data.Categories.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
	{
		var category = CreateCategory();

		category.SetName(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetName_SetsName_WhenNameIsValid(string name)
	{
		var category = CreateCategory();

		category.SetName(name);

		await Assert.That(name).IsEqualTo(category.Name);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ThrowsException_WhenNameIsInvalid(string name)
	{
		var category = CreateCategory();

		Assert.Throws<CustomValidationException<Category>>(() => category.SetName(name));
	}
}
