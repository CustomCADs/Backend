namespace CustomCADs.UnitTests.Files.Application.Images;

using CustomCADs.Shared.Domain.TypedIds.Accounts;
using static ImagesData;

public class ImagesBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Image CreateImage(string? key = null, string? contentType = null, AccountId? ownerId = null)
		=> Image.Create(key ?? ValidKey, contentType ?? ValidContentType, ownerId ?? ValidOwnerId);

	protected static Image CreateImageWithId(ImageId? id = null, string? key = null, string? contentType = null, AccountId? ownerId = null)
		=> Image.CreateWithId(id ?? ValidId, key ?? ValidKey, contentType ?? ValidContentType, ownerId ?? ValidOwnerId);
}
