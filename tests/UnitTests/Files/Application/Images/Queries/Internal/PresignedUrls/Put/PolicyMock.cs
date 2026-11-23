using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Put;

public class PolicyMock : IFileReplacePolicy<ImageId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureReplaceGrantedAsync(IFileReplacePolicy<ImageId>.FileContext context) => Task.CompletedTask;
}
