using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Domain.Cads;

using static CadsData;

public class CadsBaseUnitTests
{
	protected static Cad CreateCad(
		string? key = null,
		string? contentType = null,
		decimal? volume = null,
		Coordinates? camCoordinates = null,
		Coordinates? panCoordinates = null,
		AccountId? ownerId = null
	)
		=> Cad.Create(
			key: key ?? ValidKey,
			contentType: contentType ?? ValidContentType,
			volume: volume ?? ValidVolume,
			camCoordinates: camCoordinates ?? new(),
			panCoordinates: panCoordinates ?? new(),
			ownerId: ownerId ?? ValidOwnerId
		);

	protected static Cad CreateCadWithId(
		CadId? id = null,
		string? key = null,
		string? contentType = null,
		decimal? volume = null,
		Coordinates? camCoordinates = null,
		Coordinates? panCoordinates = null,
		AccountId? ownerId = null
	)
		=> Cad.CreateWithId(
			id: id ?? ValidId,
			key: key ?? ValidKey,
			contentType: contentType ?? ValidContentType,
			volume: volume ?? ValidVolume,
			camCoordinates: camCoordinates ?? new(),
			panCoordinates: panCoordinates ?? new(),
			ownerId: ownerId ?? ValidOwnerId
		);
}
