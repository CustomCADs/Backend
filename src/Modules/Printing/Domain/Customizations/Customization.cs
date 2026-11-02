using CustomCADs.Modules.Printing.Domain.Customizations.ValueObjects;
using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Modules.Printing.Domain.Customizations;

using static Services.PrintConstants;

public class Customization : BaseAggregateRoot
{
	private Customization() { }
	private Customization(
		decimal infill,
		Volume volume,
		string color,
		MaterialId materialId
	) : this()
	{
		Infill = infill;
		Volume = volume;
		Color = color;
		MaterialId = materialId;
	}

	public CustomizationId Id { get; init; }
	public Volume Volume { get; set; } = new();
	public decimal Infill { get; private set; }
	public string Color { get; private set; } = string.Empty;
	public MaterialId MaterialId { get; private set; }

	public static Customization Create(
		decimal infill,
		Volume volume,
		string color,
		MaterialId materialId
	) => new Customization(
		infill: infill,
		volume: volume,
		color: color,
		materialId: materialId
	)
	.ValidateInfill()
	.ValidateVolume()
	.ValidateColor();

	internal static Customization Create(
		CustomizationId id,
		decimal infill,
		Volume volume,
		string color,
		MaterialId materialId
	) => new Customization(
		infill: infill,
		volume: volume,
		color: color,
		materialId: materialId
	)
	{
		Id = id
	}
	.ValidateInfill()
	.ValidateVolume()
	.ValidateColor();

	public Customization SetInfill(decimal infill)
	{
		Infill = infill;
		this.ValidateInfill();
		return this;
	}

	public Customization SetVolume(decimal value, decimal @base, decimal scale)
	{
		Volume = Volume with { Value = value, Base = @base, Scale = scale };
		this.ValidateVolume();
		return this;
	}

	public Customization SetColor(string color)
	{
		Color = color;
		this.ValidateColor();
		return this;
	}

	public Customization SetMaterialId(MaterialId materialId)
	{
		MaterialId = materialId;
		return this;
	}

	public decimal CalculateWeight(decimal density)
		=> Volume.InCm3 * density * (WallFactor + (1 - WallFactor) * Infill);

	public decimal CalculateCost((decimal Cost, decimal Density) material)
		=> CalculateWeight(material.Density) / 1000 * material.Cost * ProfitMultiplier + ProfitBase;
}
