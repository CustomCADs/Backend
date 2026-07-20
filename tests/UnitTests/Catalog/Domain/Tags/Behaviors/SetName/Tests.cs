
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Behaviors.SetName;

public class Tests : Data.Tags.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldNotThrow_WhenNameIsValid(string name)
	{
		CreateTag().SetName(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetName_ShouldPopulateProperties_WhenNameIsValid(string name)
	{
		var tag = CreateTag();
		tag.SetName(name);
		await Assert.That(tag.Name).IsEqualTo(name);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldThrowException_WhenNameIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(() => CreateTag().SetName(name));
	}
}
