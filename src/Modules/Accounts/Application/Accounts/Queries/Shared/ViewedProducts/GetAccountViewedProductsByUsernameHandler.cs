using CustomCADs.Modules.Accounts.Domain.Accounts.Entities;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.ViewedProducts;

public sealed class GetAccountViewedProductsByUsernameHandler(IAccountReads reads)
	: IQueryHandler<GetAccountViewedProductsByUsernameQuery, ViewedProductDto[]>
{
	public async Task<ViewedProductDto[]> Handle(GetAccountViewedProductsByUsernameQuery req, CancellationToken ct)
	{
		ViewedProduct[] viewedProducts = await reads
			.ViewedProductsByUsernameAsync(req.Username, ct)
			.ConfigureAwait(false);

		return [.. viewedProducts
			.Select(x => new ViewedProductDto(x.ProductId, x.ViewedAt))
		];
	}
}
