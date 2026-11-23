using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		CreateImage();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties_WhenImageIsValid()
	{
		var image = CreateImage(ValidKey, ValidContentType);

		using (Assert.Multiple())
		{
			await Assert.That(image.Id).IsEqualTo(ValidId);
			await Assert.That(image.Key).IsEqualTo(ValidKey);
			await Assert.That(image.ContentType).IsEqualTo(ValidContentType);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenImageIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(() => CreateImage(key, contentType));
	}
}
