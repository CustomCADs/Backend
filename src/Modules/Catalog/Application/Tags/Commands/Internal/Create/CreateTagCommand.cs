namespace CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Create;

public sealed record CreateTagCommand(
	string Name
) : ICommand<TagId>;
