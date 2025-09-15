namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record GetCustomizationsCostByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, decimal>>;
