namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record BatchGetCustomizationWeightByIdQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, double>>;
