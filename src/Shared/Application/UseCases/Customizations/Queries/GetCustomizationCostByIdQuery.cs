namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record GetCustomizationCostByIdQuery(
	CustomizationId Id
) : IQuery<decimal>;
