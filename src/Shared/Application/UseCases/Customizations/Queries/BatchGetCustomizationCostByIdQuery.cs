namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record BatchGetCustomizationCostByIdQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, decimal>>;
