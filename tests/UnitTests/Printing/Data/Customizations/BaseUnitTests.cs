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
		decimal? @base = null,
		string? color = null,
		MaterialId? materialId = null,
		CustomizationId? id = null
	) => Customization.Create(
		id: id ?? ValidId,
		infill: infill ?? MinValidInfill,
		volume: new(volume ?? MinValidVolume, @base ?? MinValidBase, scale ?? MinValidScale),
		color: color ?? ValidColor,
		materialId: materialId ?? ValidMaterialId
	);

	protected static Material CreateMaterial(
		string? name = null,
		decimal? density = null,
		decimal? cost = null,
		ImageId? textureId = null,
		MaterialId? id = null
	) => Materials.BaseUnitTests.CreateMaterial(id, name, density, cost, textureId);
}
