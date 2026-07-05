using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Categories.Behaviors.Description;

public class Tests : Data.Categories.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
	{
		var category = CreateCategory();

		category.SetDescription(description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
	{
		var category = CreateCategory();

		category.SetDescription(description);

		await Assert.That(description).IsEqualTo(category.Description);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
	{
		var category = CreateCategory();

		Assert.Throws<CustomValidationException<Category>>(() => category.SetDescription(description));
	}
}
