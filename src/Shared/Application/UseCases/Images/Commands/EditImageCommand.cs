namespace CustomCADs.Shared.Application.UseCases.Images.Commands;

public sealed record EditImageCommand(
	ImageId Id,
	string ContentType
) : ICommand;
