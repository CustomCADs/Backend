using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.UnitTests.Printing.Data.Customizations;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = default;

	protected static Customization CreateCustomization(
		decimal? scale = null,
		decimal? infill = null,
		decimal? volume = null,
		string? color = null,
		MaterialId? materialId = null,
		CustomizationId? id = null
	) => Customization.Create(
		id: id ?? ValidId,
		scale: scale ?? MinValidScale,
		infill: infill ?? MinValidInfill,
		volume: volume ?? MinValidVolume,
		color: color ?? ValidColor,
		materialId: materialId ?? ValidMaterialId
	);

	protected static Material CreateMaterial(
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null,
		MaterialId? id = null
	) => Data.Materials.BaseUnitTests.CreateMaterial(id, name, density, cost, textureId);
}
