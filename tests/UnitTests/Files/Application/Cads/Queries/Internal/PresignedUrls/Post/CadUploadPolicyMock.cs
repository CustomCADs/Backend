using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;

public class CadUploadPolicyMock : IFileUploadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureUploadGrantedAsync(IFileUploadPolicy<CadId>.FileContext context) => Task.CompletedTask;
}
