using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

public class PolicyMock : IFileUploadPolicy<ImageId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureUploadGrantedAsync(IFileUploadPolicy<ImageId>.FileContext context) => Task.CompletedTask;
}
