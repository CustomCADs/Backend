namespace CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;

public sealed record GetCustomizationByIdQuery(
	CustomizationId Id
) : IQuery<CustomizationDto>;
