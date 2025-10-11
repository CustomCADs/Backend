using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Images.Commands.Internal.Create;

public sealed record CreateImageCommand(
	string GeneratedKey,
	string ContentType,
	AccountId CallerId
) : ICommand<ImageId>;
