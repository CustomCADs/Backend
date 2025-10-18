using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

public class ImageUploadPolicyMock : IFileUploadPolicy<ImageId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureUploadGrantedAsync(IFileUploadPolicy<ImageId>.FileContext context) => Task.CompletedTask;
}
