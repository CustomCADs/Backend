using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Policies;

public class ProductCadReplacePolicy(IProductReads reads) : IFileReplacePolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public async Task EnsureReplaceGrantedAsync(IFileReplacePolicy<CadId>.FileContext context)
	{
		Product product = await reads.SingleByCadIdAsync(context.FileId, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ByProp(nameof(Product.CadId), context.FileId);

		if (product.CreatorId != context.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(product.Id);
		}
	}
}
