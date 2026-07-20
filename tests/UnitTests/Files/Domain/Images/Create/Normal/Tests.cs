using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.Normal;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		Image.Create(ValidKey, ValidContentType, ValidOwnerId);
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenImageIsValid()
	{
		var image = Image.Create(ValidKey, ValidContentType, ValidOwnerId);

		using (Assert.Multiple())
		{
			await Assert.That(image.Key).IsEqualTo(ValidKey);
			await Assert.That(image.ContentType).IsEqualTo(ValidContentType);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(() => Image.Create(key, contentType, ValidOwnerId));
	}
}
