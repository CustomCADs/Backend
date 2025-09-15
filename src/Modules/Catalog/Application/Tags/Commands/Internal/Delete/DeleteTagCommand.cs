namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Delete;

public sealed record DeleteTagCommand(
	TagId Id
) : ICommand;
