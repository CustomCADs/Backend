using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.ContentType;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	[Test]
	public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid()
	{
		var cad = CreateCad();

		cad.SetContentType(ValidContentType);
	}

	[Test]
	public async Task SetContentType_ShouldPopulateProperties_WhenContentTypeIsValid()
	{
		var cad = CreateCad();

		cad.SetContentType(ValidContentType);

		await Assert.That(cad.ContentType).IsEqualTo(ValidContentType);
	}

	[Test]
	public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(() => cad.SetContentType(InvalidContentType));
	}
}
