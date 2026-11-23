using CustomCADs.Modules.Printing.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Printing.Application.Customizations.Queries.Internal.GetById;

public sealed class GetCustomizationByIdHandler(
	ICustomizationReads customizationReads,
	IMaterialReads materialReads
) : IQueryHandler<GetCustomizationByIdQuery, CustomizationDto>
{
	public async Task<CustomizationDto> Handle(GetCustomizationByIdQuery req, CancellationToken ct)
	{
		Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

		return customization.ToDto(
			weight: customization.CalculateWeight(material.Density),
			cost: customization.CalculateCost((material.Cost, material.Density))
		);
	}
}
