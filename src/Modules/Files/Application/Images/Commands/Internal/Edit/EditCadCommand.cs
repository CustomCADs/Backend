using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Images.Commands.Internal.Edit;

public sealed record EditImageCommand(
	ImageId Id,
	string ContentType,
	AccountId CallerId
) : ICommand;
