using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Images.Behaviors.ContentType;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	[Test]
	public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid()
	{
		var image = CreateImage();

		image.SetContentType(ValidContentType);
	}

	[Test]
	public async Task SetContentType_ShouldPopulateProperties_WhenContentTypeIsValid()
	{
		var image = CreateImage();

		image.SetContentType(ValidContentType);

		await Assert.That(image.ContentType).IsEqualTo(ValidContentType);
	}

	[Test]
	public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid()
	{
		var image = CreateImage();

		Assert.Throws<CustomValidationException<Image>>(() => image.SetContentType(InvalidContentType));
	}
}
