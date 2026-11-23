using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Printing.Data.Materials;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = default;

	protected static Material CreateMaterial(
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null,
		MaterialId? id = null
	) => Create(id, name, density, cost, textureId);

	internal static Material CreateMaterial(
		MaterialId? id = null,
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null
	) => Create(id, name, density, cost, textureId);

	private static Material Create(
		MaterialId? id = null,
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null
	) => Material.Create(
			id: id ?? ValidId,
			name: name ?? MinValidName,
			density: density ?? MinValidDensity,
			cost: cost ?? MinValidCost,
			textureId: textureId ?? ValidTextureId
		);
}
