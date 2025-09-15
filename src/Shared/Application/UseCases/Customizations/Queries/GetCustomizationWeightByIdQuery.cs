namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record GetCustomizationWeightByIdQuery(
	CustomizationId Id
) : IQuery<double>;
