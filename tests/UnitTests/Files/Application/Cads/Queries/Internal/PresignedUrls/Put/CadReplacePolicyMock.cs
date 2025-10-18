using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;

public class CadReplacePolicyMock : IFileReplacePolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public Task EnsureReplaceGrantedAsync(IFileReplacePolicy<CadId>.FileContext context) => Task.CompletedTask;
}
