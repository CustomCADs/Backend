using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Modules.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Data.Cads;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Cad CreateCad(
		string key = ValidKey,
		string contentType = ValidContentType,
		decimal volume = ValidVolume,
		Coordinates? camCoordinates = null,
		Coordinates? panCoordinates = null,
		AccountId? ownerId = null,
		CadId? id = null
	)
		=> Cad.CreateWithId(
			id: id ?? ValidId,
			key: key,
			contentType: contentType,
			volume: volume,
			camCoordinates: camCoordinates ?? new(),
			panCoordinates: panCoordinates ?? new(),
			ownerId: ownerId ?? ValidOwnerId
		);
}
