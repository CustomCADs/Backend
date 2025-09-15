using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Cost;

public sealed class GetCustomizationsCostByIdsHandler(
	ICustomizationReads customizationReads,
	IMaterialReads materialReads,
	IPrintCalculator calculator
) : IQueryHandler<GetCustomizationsCostByIdsQuery, Dictionary<CustomizationId, decimal>>
{
	public async Task<Dictionary<CustomizationId, decimal>> Handle(GetCustomizationsCostByIdsQuery req, CancellationToken ct)
	{
		ICollection<Customization> customizations = await customizationReads.AllAsync(req.Ids, track: false, ct).ConfigureAwait(false);
		MaterialId[] materialIds = [.. customizations.Select(x => x.MaterialId).Distinct()];

		Dictionary<MaterialId, Material> materials = await materialReads.AllByIdsAsync(materialIds, track: false, ct).ConfigureAwait(false);
		return customizations.ToDictionary(
			x => x.Id,
			x =>
			{
				Customization customization = x;
				Material material = materials[customization.MaterialId];
				return calculator.CalculateCost(customization, material);
			}
		);
	}
}
