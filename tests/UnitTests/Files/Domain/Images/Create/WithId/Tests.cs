using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.UnitTests.Files.Data.Images;

namespace CustomCADs.UnitTests.Files.Domain.Images.Create.WithId;

using static Data.Images.TestData;

public class Tests : Data.Images.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenImageIsValid()
	{
		CreateImage();
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenImageIsValid()
	{
		var image = CreateImage(ValidKey, ValidContentType);

		Assert.Multiple(
			() => Assert.Equal(ValidKey, image.Key),
			() => Assert.Equal(ValidContentType, image.ContentType)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenImageIsInvalid(string key, string contentType)
	{
		Assert.Throws<CustomValidationException<Image>>(
			() => CreateImage(key, contentType)
		);
	}
}
