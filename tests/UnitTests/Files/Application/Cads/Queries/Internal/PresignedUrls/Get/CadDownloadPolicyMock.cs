using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;

public class CadDownloadPolicyMock : IFileDownloadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context) => Task.CompletedTask;
}
