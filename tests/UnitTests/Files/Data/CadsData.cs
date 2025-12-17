using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Data;

using static CadConstants.Coordinates;

public class CadsData
{
	public const string ValidKey = "key-to-cad";
	public const string InvalidKey = "";

	public const string ValidContentType = "model/gltf+json";
	public const string InvalidContentType = "";

	public const decimal ValidVolume = 1000;
	public const decimal InvalidVolume = 0;

	public const decimal MinValidCoord = CoordMin + 1;
	public const decimal MaxValidCoord = CoordMax - 1;
	public const decimal MinInvalidCoord = CoordMin - 1;
	public const decimal MaxInvalidCoord = CoordMax + 1;

	public static readonly Coordinates ValidCoords = new(MinValidCoord, MinValidCoord, MinValidCoord);
	public static readonly Coordinates MinInvalidCoords = new(MinInvalidCoord, MinInvalidCoord, MinInvalidCoord);
	public static readonly Coordinates MaxInvalidCoords = new(MaxInvalidCoord, MaxInvalidCoord, MaxInvalidCoord);

	public static readonly CadId ValidId = CadId.New();
	public static readonly AccountId ValidOwnerId = AccountId.New();
}
