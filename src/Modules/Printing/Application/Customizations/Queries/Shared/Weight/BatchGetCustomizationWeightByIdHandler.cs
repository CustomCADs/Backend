using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Weight;

public sealed class BatchGetCustomizationWeightByIdHandler(
	ICustomizationReads customizationReads,
	IMaterialReads materialReads
) : IQueryHandler<BatchGetCustomizationWeightByIdQuery, Dictionary<CustomizationId, double>>
{
	public async Task<Dictionary<CustomizationId, double>> Handle(BatchGetCustomizationWeightByIdQuery req, CancellationToken ct)
	{
		ICollection<Customization> customizations = await customizationReads.AllAsync(req.Ids, track: false, ct).ConfigureAwait(false);
		MaterialId[] materialIds = [.. customizations.Select(x => x.MaterialId).Distinct()];

		Dictionary<MaterialId, Material> materials = await materialReads.AllByIdsAsync(materialIds, track: false, ct).ConfigureAwait(false);
		return customizations.ToDictionary(
			x => x.Id,
			x =>
			{
				Customization customization = x;
				Material material = materials[x.MaterialId];

				decimal weight = customization.CalculateWeight(material.Density);
				return Convert.ToDouble(weight);
			}
		);
	}
}
