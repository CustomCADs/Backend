namespace CustomCADs.Shared.Application.UseCases.Customizations.Commands;

public sealed record DeleteCustomizationByIdCommand(CustomizationId Id) : ICommand;
