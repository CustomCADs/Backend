using CustomCADs.Modules.Files.Domain.Images;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Data.Images;

using static TestData;

public class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Image CreateImage(
		string? key = null,
		string? contentType = null,
		AccountId? ownerId = null,
		ImageId? id = null
	)
		=> Image.CreateWithId(
			id: id ?? ValidId,
			key: key ?? ValidKey,
			contentType: contentType ?? ValidContentType,
			ownerId: ownerId ?? ValidOwnerId
		);
}
