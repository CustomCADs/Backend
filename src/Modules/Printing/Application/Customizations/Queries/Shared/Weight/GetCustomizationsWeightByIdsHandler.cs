using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Weight;

public sealed class GetCustomizationsWeightByIdsHandler(
	ICustomizationReads customizationReads,
	IMaterialReads materialReads,
	IPrintCalculator calculator
) : IQueryHandler<GetCustomizationsWeightByIdsQuery, Dictionary<CustomizationId, double>>
{
	public async Task<Dictionary<CustomizationId, double>> Handle(GetCustomizationsWeightByIdsQuery req, CancellationToken ct)
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

				decimal weight = calculator.CalculateWeight(customization, material);
				return Convert.ToDouble(weight);
			}
		);
	}
}
