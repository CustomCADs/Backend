namespace CustomCADs.UnitTests.Files.Application.Cads;

using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using static CadsData;

public class CadsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Cad CreateCad(string? key = null, string? contentType = null, decimal? volume = null, Coordinates? camCoordinates = null, Coordinates? panCoordinates = null, AccountId? ownerId = null)
		=> Cad.Create(key ?? ValidKey, contentType ?? ValidContentType, volume ?? ValidVolume, camCoordinates ?? ValidCoords, panCoordinates ?? ValidCoords, ownerId ?? ValidOwnerId);

	protected static Cad CreateCadWithId(CadId? id = null, string? key = null, string? contentType = null, decimal? volume = null, Coordinates? camCoordinates = null, Coordinates? panCoordinates = null, AccountId? ownerId = null)
		=> Cad.CreateWithId(id ?? ValidId, key ?? ValidKey, contentType ?? ValidContentType, volume ?? ValidVolume, camCoordinates ?? ValidCoords, panCoordinates ?? ValidCoords, ownerId ?? ValidOwnerId);
}
