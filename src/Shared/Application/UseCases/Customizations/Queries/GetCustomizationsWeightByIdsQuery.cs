namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record GetCustomizationsWeightByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, double>>;
