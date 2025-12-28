using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Catalog.Application.Products.Policies;

public class ProductCadDownloadPolicy(IProductReads reads) : IFileDownloadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public async Task EnsureDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context)
	{
		Product product = await reads.SingleByCadIdAsync(context.FileId, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ByProp(nameof(Product.CadId), context.FileId);

		if (product.Status is ProductStatus.Validated)
		{
			return; // is publicly available
		}

		if (product.CreatorId != context.CallerId || product.DesignerId != context.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(product.Id);
		}
	}
}
