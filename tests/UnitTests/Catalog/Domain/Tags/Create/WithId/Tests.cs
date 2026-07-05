using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Catalog.Domain.Tags.Create.WithId;

using static Data.Tags.TestData;

public class Tests : Data.Tags.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenProductIsValid(string name)
	{
		CreateTag(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties_WhenProductIsValid(string name)
	{
		Tag tag = CreateTag(name, ValidId);

		using (Assert.Multiple())
		{
			await Assert.That(tag.Id).IsEqualTo(ValidId);
			await Assert.That(tag.Name).IsEqualTo(name);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenProductIsNotValid(string name)
	{
		Assert.Throws<CustomValidationException<Tag>>(() => CreateTag(name));
	}
}
