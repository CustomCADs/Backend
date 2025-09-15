namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public sealed record GetCustomizationExistsByIdQuery(
	CustomizationId Id
) : IQuery<bool>;
