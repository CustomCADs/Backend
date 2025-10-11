using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Domain.Images;

using static ImagesData;

public class ImagesBaseUnitTests
{
	protected static Image CreateImage(string? key = null, string? contentType = null, AccountId? ownerId = null)
		=> Image.Create(key ?? ValidKey, contentType ?? ValidContentType, ownerId ?? ValidOwnerId);

	protected static Image CreateImageWithId(ImageId? id = null, string? key = null, string? contentType = null, AccountId? ownerId = null)
		=> Image.CreateWithId(id ?? ImageId.New(), key ?? ValidKey, contentType ?? ValidContentType, ownerId ?? ValidOwnerId);
}
