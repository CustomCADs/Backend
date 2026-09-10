using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Weight;

public sealed class GetCustomizationWeightByIdHandler(
	ICustomizationReads customizationReads,
	IMaterialReads materialReads
) : IQueryHandler<GetCustomizationWeightByIdQuery, double>
{
	public async Task<double> Handle(GetCustomizationWeightByIdQuery req, CancellationToken ct)
	{
		Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

		return Convert.ToDouble(customization.CalculateWeight(material.Density));
	}
}
