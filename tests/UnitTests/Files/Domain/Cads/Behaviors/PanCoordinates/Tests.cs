using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.PanCoordinates;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private static readonly Coordinates Coords = new(MinValidCoord, MinValidCoord, MinValidCoord);

	[Test]
	public void SetPanCoordinates_ShouldNotThrowException_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(Coords);
	}

	[Test]
	public async Task SetPanCoordinates_ShouldPopulateProperties_WhenCoordinatesAreValid()
	{
		var cad = CreateCad();

		cad.SetPanCoordinates(Coords);

		await Assert.That(cad.PanCoordinates).IsEqualTo(Coords);
	}

	[Test]
	public void SetPanCoordinates_ShouldThrowException_WhenCoordinatesIsInvalid()
	{
		var cad = CreateCad();

		Assert.Throws<CustomValidationException<Cad>>(() => cad.SetPanCoordinates(Coords with { X = MaxInvalidCoord }));
	}
}
