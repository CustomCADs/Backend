using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.CamCoordinates;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private static readonly Coordinates Coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void SetCamCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetCamCoordinates(Coords);
	}

	[Fact]
	public void SetCamCoordinates_ShouldPopulateProperties_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetCamCoordinates(Coords);

		Assert.Equal(Coords, cad.CamCoordinates);
	}

	[Fact]
	public void SetCamCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetCamCoordinates(Coords with { X = MinInvalidCoord })
		);
	}
}
