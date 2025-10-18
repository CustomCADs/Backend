using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Get;

public class ImageDownloadPolicyMock : IFileDownloadPolicy<ImageId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureDownloadGrantedAsync(IFileDownloadPolicy<ImageId>.FileContext context) => Task.CompletedTask;
}
