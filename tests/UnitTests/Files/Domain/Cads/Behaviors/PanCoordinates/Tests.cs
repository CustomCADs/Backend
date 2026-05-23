using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private static readonly Coordinates Coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Fact]
	public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(Coords);
	}

	[Fact]
	public void SetPanCoordinates_ShouldPopulateProperties_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(Coords);

		Assert.Equal(Coords, cad.PanCoordinates);
	}

	[Fact]
	public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(
			() => cad.SetPanCoordinates(Coords with { X = MaxInvalidCoord })
		);
	}
}
