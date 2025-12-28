namespace CustomCADs.Modules.Files.Domain.Cads;

using static CadConstants;

internal static class Validations
{
	private const string BaseMessage = "A/An {0}'s {1} must be";

	extension(Cad cad)
	{
		internal Cad ValidateKey()
			=> cad
				.ThrowIfNull(
					expression: x => x.Key,
					predicate: string.IsNullOrWhiteSpace
			);

		internal Cad ValidateContentType()
			=> cad
				.ThrowIfNull(
					expression: x => x.ContentType,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Cad ValidateVolume()
			=> cad
				.ThrowIfInvalidRange(
					expression: x => x.Volume,
					range: (VolumeMin, VolumeMax),
					inclusive: true
				);

		internal Cad ValidateCamCoordinates()
			=> cad
				.ThrowIfPredicateIsFalse(
					expression: x => x.CamCoordinates,
					predicate: AreCoordsValid,
					message: $"{BaseMessage} more than {VolumeMin} and less than {VolumeMax}."
				);

		internal Cad ValidatePanCoordinates()
			=> cad
				.ThrowIfPredicateIsFalse(
					expression: x => x.PanCoordinates,
					predicate: AreCoordsValid,
					message: $"{BaseMessage} more than {VolumeMin} and less than {VolumeMax}."
				);
	}

	private static bool AreCoordsValid(ValueObjects.Coordinates coordinates)
		=> coordinates.X.IsValidCoord() && coordinates.Y.IsValidCoord() && coordinates.Z.IsValidCoord();

	private static bool IsValidCoord(this decimal coord)
		=> coord > Coordinates.CoordMin && coord < Coordinates.CoordMax;
}
