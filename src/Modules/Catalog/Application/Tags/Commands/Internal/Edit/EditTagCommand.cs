namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

public sealed record EditTagCommand(
	TagId Id,
	string Name
) : ICommand;
